using UniRx;
using UnityEngine;
using Assets.Source.Config;
using Assets.Source.Entities.Items.Config;

namespace Assets.Source.Behaviours.Items
{
    public class ItemSpawner : AbstractBehaviour
    {
        public SpawningLanesConfig spawningLanesConfig;

        private bool CanSpawn = true;

        protected int lastSpawnPoint = 0;
        protected int distanceSinceLastSpawn = 0;
        
        protected float offsetX;
        protected Vector3 groundPosition;
        protected System.Random randomPoolIndex;


        private void Start()
        {
            randomPoolIndex = new System.Random();
            offsetX = goTransform.position.x - App.Cache.Jester.goTransform.position.x;
            groundPosition = goTransform.position;

            // Deactivate on Land                        
            App.Cache.Jester.IsLandedProperty.Where(e => e).Subscribe(_ => { CanSpawn = false; }).AddTo(this);

            // Attempt to spawn based on travel distance
            App.Cache.Jester.DistanceProperty                                 
                            .Subscribe(AttemptSpawn)
                            .AddTo(this);
        }


        // Checks if Spawn should occur and Spawns object
        protected virtual void AttemptSpawn(float distance)
        {
            if (!CanSpawn) { return; }

            // Try a spawn in each of the lanes
            for (var i=0; i < spawningLanesConfig.SpawningLanes.Count; i++)
            {
                SpawningLane spawningLane = spawningLanesConfig.SpawningLanes[i];

                if (ShouldSpawn((int)distance, spawningLane))
                {
                    SpawnRandomItem(spawningLane);
                }
            }
        }


        /// <summary>
        /// <para> Checks whether we should spawn an item, based on some rules</para>
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        protected virtual bool ShouldSpawn(int distance, SpawningLane spawningLane)
        {
            // Sanity check
            if (spawningLane.itemPool.Count == 0)
            {
                Debug.LogWarning("Item Pool is empty! Please verify if everything is correctly setup in the spawners");
                return false;
            }

            // Do not spawn if the jester is above the lane max height and is also moving upwards
            if (!spawningLanesConfig.SpawnOnTrajectory &&
                App.Cache.Jester.goBody.position.y > spawningLane.maxHeight &&
                App.Cache.Jester.goBody.velocity.y > 0)
            {
                return false;
            }

            distanceSinceLastSpawn = distance - lastSpawnPoint;

            // Do not spawn if the minimum distance since last Spawn was not travelled yet
            if (distanceSinceLastSpawn <= spawningLanesConfig.MinDistanceBetweenSpawns)
            {
                return false;
            }
            
            // Randomly decide whether to spawn
            bool result = spawningLanesConfig.SpawnChance >= Random.Range(0.01f, 1.0f);

            if (result)
            {
                // Reset distance tracking
                lastSpawnPoint = distance;
                distanceSinceLastSpawn = 0;
            }

            return result;
        }


        // Spawns a random item from the pool
        protected virtual void SpawnRandomItem(SpawningLane spawningLane)
        {
            var itemPool = spawningLane.itemPool;
            int index = randomPoolIndex.Next(0, itemPool.Count);

            GameObject go = Instantiate(itemPool[index]);
            go.transform.position = GetSpawnPosition(spawningLane);
        }


        // Returns the spawn position, based on the set config
        protected virtual Vector2 GetSpawnPosition(SpawningLane spawningLane)
        {
            if (spawningLanesConfig.SpawnOnTrajectory)
            {
                return GetProjectedSpawnPosition();
            }

            return GetSpawnPositionInsideLane(spawningLane);
        }

        /// <summary>
        /// Calculates the spawn position, according to the spawning lane restrictions
        /// </summary>
        /// <param name="spawningLane"></param>
        /// <returns></returns>
        private Vector2 GetSpawnPositionInsideLane(SpawningLane spawningLane)
        {
            // Random spread within the lane
            float randomPosY = Random.Range(spawningLane.minHeight, spawningLane.maxHeight);
            
            // Introduce deviation on x position
            float deviationX = Random.Range(-spawningLanesConfig.MaxDeviation,
                spawningLanesConfig.MaxDeviation);

            var spawnPosition = new Vector2(goTransform.position.x + deviationX, groundPosition.y + randomPosY);
            return spawnPosition;
        }


        // Calculates the Jester's projected position for spawning
        // Adds deviation from the calculated position, based on config above
        // Position is capped at ground level        
        private Vector2 GetProjectedSpawnPosition()
        {
            Vector2 projectedPosition = GetProjectedPosition();

            // Introduce deviation
            float deviationX = Random.Range(-spawningLanesConfig.MaxDeviation,
                spawningLanesConfig.MaxDeviation);
            float deviationY = Random.Range(-spawningLanesConfig.MaxDeviation,
                spawningLanesConfig.MaxDeviation);

            projectedPosition.Set(projectedPosition.x + deviationX, projectedPosition.y + deviationY);

            // Cap projected position at min Height
            if (projectedPosition.y <= spawningLanesConfig.MinHeight)
            {
                projectedPosition.Set(projectedPosition.x, spawningLanesConfig.MinHeight);
            }

            return projectedPosition;
        }


        private Vector2 GetProjectedPosition()
        {
            Vector2 jesterVelocity = App.Cache.Jester.goBody.velocity;
            Vector2 jesterPos = App.Cache.Jester.goTransform.position;
            Vector2 v2Gravity = Physics.gravity;

            // The jester's travel-time from his current position to the Spawners X
            float time = offsetX / jesterVelocity.x;

            // Calculate position on trajectory at the spawners X
            Vector2 projected = jesterPos + (jesterVelocity * time) + (v2Gravity * time * time * 0.5f);

            return projected;
        }
    }
}
