using UniRx;
using UnityEngine;
using Assets.Source.App;

namespace Assets.Source.Behaviours
{
    public class ItemSpawner : AbstractBehaviour
    {
        private bool CanSpawn = true;

        public GameObject[] ItemPool;

        // Percent-based Spawn chance
        [Range(0.0f, 1.0f)]
        public float SpawnChance = 0.8f;

        // If this is set, item will be spanwed on the Jesters projected trajectory
        public bool SpawnOnTrajectory = true;
        private float minTrajectorySpawnHeight = 5f;

        // Determines by how much the Spawn location can deviate from the projected position
        // This has no effect if SpawnOnTrajectory = false
        [Range(0.0f, 15)]
        public float ProjectionMaxDeviation = 1f;

        [Range(0, 10000)]
        public int MinDistanceBetweenSpawns = 20;

        protected int lastSpawnPoint = 0;
        protected int distanceSinceLastSpawn = 0;
        
        protected float offsetX;
        protected Vector3 groundPosition;
        protected System.Random randomPoolIndex;


        private void Start()
        {
            randomPoolIndex = new System.Random();
            offsetX = goTransform.position.x - App.Cache.jester.goTransform.position.x;
            groundPosition = goTransform.position;

            // Deactivate on Land                        
            App.Cache.jester.IsLandedProperty.Where(e => e).Subscribe(_ => { CanSpawn = false; }).AddTo(this);

            // Attempt to spawn based on travel distance
            App.Cache.jester.DistanceProperty                                 
                            .Subscribe(AttemptSpawn)
                            .AddTo(this);
        }


        // Checks if Spawn should occur and Spawns object
        protected virtual void AttemptSpawn(float distance)
        {
            if (CanSpawn && ShouldSpawn(distance.ToMeters()))
            {
                SpawnRandomItem();
            }
        }


        // Checks whether we should spawn an obstacle, based on some rules
        protected virtual bool ShouldSpawn(int distance)
        {
            // Do not spawn if we should spawn on the ground and jester is moving upwards
            if (!SpawnOnTrajectory && App.Cache.jester.goBody.velocity.y > 0)
            {
                return false;
            }

            distanceSinceLastSpawn = distance - lastSpawnPoint;

            // Check if the minimum Distance since last Spawn was travelled
            if (distanceSinceLastSpawn >= MinDistanceBetweenSpawns)
            {
                // Randomly decide whether to spawn
                bool result = SpawnChance >= UnityEngine.Random.Range(0.01f, 1.0f);

                if (result)
                {
                    // Reset distance tracking
                    lastSpawnPoint = distance;
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

            // Cap projected position at min Height
            if (projectedPosition.y <= minTrajectorySpawnHeight)
            {
                projectedPosition.Set(projectedPosition.x, minTrajectorySpawnHeight);
            }

            return projectedPosition;
        }


        private Vector2 GetProjectedPosition()
        {
            Vector2 jesterVelocity = App.Cache.jester.goBody.velocity;
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
