﻿using UniRx;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Assets.Source.Config;

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
            if (CanSpawn && ShouldSpawn((int)distance))
            {
                SpawnRandomItem();
            }
        }


        // Checks whether we should spawn an obstacle, based on some rules
        protected virtual bool ShouldSpawn(int distance)
        {
            // Do not spawn if we should spawn on the ground and jester is moving upwards
            if (!spawningLanesConfig.SpawnOnTrajectory && App.Cache.Jester.goBody.velocity.y > 0)
            {
                return false;
            }

            distanceSinceLastSpawn = distance - lastSpawnPoint;

            // Check if the minimum Distance since last Spawn was travelled
            if (distanceSinceLastSpawn >= spawningLanesConfig.MinDistanceBetweenSpawns)
            {
                // Randomly decide whether to spawn
                bool result = spawningLanesConfig.SpawnChance >= UnityEngine.Random.Range(0.01f, 1.0f);

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
            // TODO: has to get one spawning lane instance
            SpawningLane spawningLane = spawningLanesConfig.SpawningLanes[0];

            int index = randomPoolIndex.Next(0, spawningLane.itemPool.Count);

            GameObject go = Instantiate(spawningLane.itemPool[index]);
            go.transform.position = GetSpawnPosition();
        }


        // Returns the spawn position, based on the set config
        protected virtual Vector2 GetSpawnPosition()
        {
            if (spawningLanesConfig.SpawnOnTrajectory)
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
            float deviationX = UnityEngine.Random.Range(-spawningLanesConfig.ProjectionMaxDeviation, spawningLanesConfig.ProjectionMaxDeviation);
            float deviationY = UnityEngine.Random.Range(-spawningLanesConfig.ProjectionMaxDeviation, spawningLanesConfig.ProjectionMaxDeviation);

            projectedPosition.Set(projectedPosition.x + deviationX, projectedPosition.y + deviationY);

            // Cap projected position at min Height
            if (projectedPosition.y <= spawningLanesConfig.minimumHeight)
            {
                projectedPosition.Set(projectedPosition.x, spawningLanesConfig.minimumHeight);
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
