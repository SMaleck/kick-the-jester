using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class ItemSpawner : BaseEntity
    {
        public GameObject[] ItemPool;

        // Percent-based Spawn chance
        [Range(0.0f, 1.0f)]
        public float SpawnChance = 0.8f;

        // If this is set, item will be spanwed on the Jesters projected trajectory
        public bool SpawnOnTrajectory = true;

        // Determines by how much the Spawn location can deviate from the projected position
        // This has no effect if SpawnOnTrajectory = false
        [Range(0.0f, 15)]
        public float ProjectionMaxDeviation = 1f;

        [Range(0, 10000)]
        public int MinDistanceBetweenSpawns = 200;

        protected int lastSpawnPoint = 0;
        protected int distanceSinceLastSpawn = 0;
        
        protected float offsetX;
        protected Vector3 groundPosition;
        protected System.Random randomPoolIndex;


        public virtual void Awake()
        {
            randomPoolIndex = new System.Random();
            offsetX = goTransform.position.x - App.Cache.jester.goTransform.position.x;
            groundPosition = goTransform.position;

            App.Cache.rxState.AttachForFlightStats(OnFlightStatsChanged);
        }


        // Checks if Spawn should occur and Spawns object
        protected virtual void OnFlightStatsChanged(FlightStats stats)
        {           
            if (ShouldSpawn(stats))
            {
                SpawnRandomItem();
            }
        }


        // Checks whether we should spawn an obstacle, based on some rules
        protected virtual bool ShouldSpawn(FlightStats stats)
        {
            // Do not spawn if Jester is not moving
            if (stats.IsLanded || stats.Velocity.x <= 0)
            {
                return false;
            }

            // Do not spawn if we should spawn on the ground and jester is moving upwards
            if(!SpawnOnTrajectory && stats.Velocity.y < 0)
            {
                return false;
            }

            distanceSinceLastSpawn = stats.Distance - lastSpawnPoint;

            // Check if the minimum Distance since last Spawn was travelled
            if (distanceSinceLastSpawn >= MinDistanceBetweenSpawns)
            {
                // Randomly decide whether to spawn
                bool result = SpawnChance >= UnityEngine.Random.Range(0.01f, 1.0f);

                if (result)
                {
                    // Reset distance tracking
                    lastSpawnPoint = stats.Distance;
                    distanceSinceLastSpawn = 0;
                }

                return result;
            }

            return false;
        }


        // Spawns a random item from the pool
        protected virtual void SpawnRandomItem()
        {
            int index = randomPoolIndex.Next(0, ItemPool.Length);

            GameObject go = Instantiate(ItemPool[index]);
            go.transform.position = GetSpawnPosition();
        }


        // Returns the spawn position, based on the set config
        protected virtual Vector2 GetSpawnPosition()
        {
            if (SpawnOnTrajectory)
            {
                return GetProjectedSpawnPosition();
            }

            return new Vector2(goTransform.position.x, groundPosition.y);
        }


        // Claculates the Jester's projetced position for spawning
        // Adds deviation from the calculated position, based on config above
        // Position is capped at ground level        
        private Vector2 GetProjectedSpawnPosition()
        {
            Vector2 projectedPosition = GetProjectedPosition();

            // Introduce deviation
            float deviationX = UnityEngine.Random.Range(-ProjectionMaxDeviation, ProjectionMaxDeviation);
            float deviationY = UnityEngine.Random.Range(-ProjectionMaxDeviation, ProjectionMaxDeviation);

            projectedPosition.Set(projectedPosition.x + deviationX, projectedPosition.y + deviationY);

            // Cap projected position at ground level
            if (projectedPosition.y <= groundPosition.y)
            {
                projectedPosition.Set(projectedPosition.x, groundPosition.y);
            }

            return projectedPosition;
        }


        private Vector2 GetProjectedPosition()
        {
            Vector2 jesterVelocity = App.Cache.jester.Body.velocity;
            Vector2 jesterPos = App.Cache.jester.goTransform.position;
            Vector2 v2Gravity = Physics.gravity;

            // The jester's travel-time from his current position to the Spawners X
            float time = offsetX / jesterVelocity.x;

            // Calculate position on trajectory at the spawners X
            Vector2 projected = jesterPos + (jesterVelocity * time) + (v2Gravity * time * time * 0.5f);

            return projected;
        }
    }
}
