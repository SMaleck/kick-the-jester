using Assets.Source.Entities.Items;
using Assets.Source.Features.PickupItems.Data;
using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Entities;
using UniRx;
using UnityEngine;
using Logger = Assets.Source.App.Logger;
using Random = UnityEngine.Random;

namespace Assets.Source.Features.PickupItems
{
    public class PickupItemSpawner : AbstractDisposable
    {
        private readonly IWorldObjectSpawnData _worldObjectSpawnData;
        private readonly AbstractItemEntity.Factory _abstractItemEntityFactory;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly SpawnAnchorEntity _spawnAnchor;

        private readonly Dictionary<SpawnLane, int> _lastSpawnPoints;
        protected float OffsetX => _spawnAnchor.Position.x - _flightStatsModel.Position.Value.x;

        public PickupItemSpawner(
            IWorldObjectSpawnData worldObjectSpawnData,
            AbstractItemEntity.Factory abstractItemEntityFactory,
            FlightStatsModel flightStatsModel,
            SpawnAnchorEntity spawnAnchor)
        {
            _worldObjectSpawnData = worldObjectSpawnData;
            _abstractItemEntityFactory = abstractItemEntityFactory;
            _flightStatsModel = flightStatsModel;
            _spawnAnchor = spawnAnchor;

            _lastSpawnPoints = _worldObjectSpawnData.SpawnLanes
                .ToDictionary(item => item, item => 0);

            _flightStatsModel.DistanceUnits
                .Where(_ => !_flightStatsModel.IsLanded.Value)
                .Subscribe(AttemptSpawn)
                .AddTo(Disposer);
        }

        // Checks if Spawn should occur and Spawns object
        protected virtual void AttemptSpawn(float distanceUnits)
        {
            foreach (var spawnLane in _worldObjectSpawnData.SpawnLanes)
            {
                if (ShouldSpawn((int)distanceUnits, spawnLane))
                {
                    SpawnRandomItem(spawnLane);
                }
            }
        }

        protected virtual bool ShouldSpawn(int distance, SpawnLane spawnLane)
        {
            if (spawnLane.ItemPool.Count == 0)
            {
                Logger.Error("Item Pool cannot be empty!");
                return false;
            }

            // Do not spawn if the jester is above the lane max height and is also moving upwards
            if (!spawnLane.SpawnOnTrajectory &&
                _flightStatsModel.Position.Value.y > spawnLane.MaxHeightUnits &&
                _flightStatsModel.Velocity.Value.y > 0)
            {
                return false;
            }

            var distanceSinceLastSpawn = distance - _lastSpawnPoints[spawnLane];

            // Do not spawn if the minimum distance since last Spawn was not travelled yet
            if (distanceSinceLastSpawn <= spawnLane.MinDistanceBetweenSpawnsUnits)
            {
                return false;
            }

            // Randomly decide whether to spawn
            var result = spawnLane.LaneSpawnChance >= Random.Range(0.01f, 1.0f);

            if (result)
            {
                // Reset distance tracking
                _lastSpawnPoints[spawnLane] = distance;
            }

            return result;
        }

        // Spawns a random item from the pool
        protected virtual void SpawnRandomItem(SpawnLane spawnLane)
        {
            var itemPool = spawnLane.ItemPool;
            int index = Random.Range(0, itemPool.Count);

            var prefab = _worldObjectSpawnData.GetPrefab(itemPool[index]);
            var item = _abstractItemEntityFactory.Create(prefab);

            item.Position = GetSpawnPosition(spawnLane);
            item.Initialize();
        }


        // Returns the spawn position, based on the set config
        protected virtual Vector2 GetSpawnPosition(SpawnLane spawnLane)
        {
            if (spawnLane.SpawnOnTrajectory)
            {
                return GetProjectedSpawnPosition(spawnLane);
            }

            return GetSpawnPositionInsideLane(spawnLane);
        }

        private Vector2 GetSpawnPositionInsideLane(SpawnLane spawnLane)
        {
            // Random spread within the lane
            float randomPosY = Random.Range(
                spawnLane.MinHeightUnits,
                spawnLane.MaxHeightUnits);

            // Introduce deviation on x position
            float deviationX = Random.Range(
                -spawnLane.MaxDeviation,
                spawnLane.MaxDeviation);

            return new Vector2(
                _spawnAnchor.Position.x + deviationX,
                _spawnAnchor.Position.y + randomPosY);
        }

        // Calculates the Jester's projected position for spawning
        // Adds deviation from the calculated position, based on config above
        // Position is capped at ground level
        private Vector2 GetProjectedSpawnPosition(SpawnLane spawnLane)
        {
            Vector2 projectedPosition = GetProjectedPosition();

            // Introduce deviation
            float deviationX = Random.Range(
                -spawnLane.MaxDeviation,
                spawnLane.MaxDeviation);

            float deviationY = Random.Range(
                -spawnLane.MaxDeviation,
                spawnLane.MaxDeviation);

            projectedPosition.Set(
                projectedPosition.x + deviationX,
                projectedPosition.y + deviationY);

            // Cap projected position at min Height
            if (projectedPosition.y <= spawnLane.MinHeightUnits)
            {
                projectedPosition.Set(
                    projectedPosition.x,
                    spawnLane.MinHeightUnits);
            }

            return projectedPosition;
        }

        private Vector2 GetProjectedPosition()
        {
            Vector2 velocity = _flightStatsModel.Velocity.Value;
            Vector2 position = _flightStatsModel.Position.Value;
            Vector2 v2Gravity = Physics.gravity;

            // The jester's travel-time from his current position to the Spawners X
            float time = OffsetX / velocity.x;

            // Calculate position on trajectory at the spawners X
            Vector2 projected = position + (velocity * time) + (v2Gravity * time * time * 0.5f);

            return projected;
        }
    }
}
