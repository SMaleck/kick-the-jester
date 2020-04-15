using Assets.Source.Entities.Items.Config;
using Assets.Source.Entities.Jester;
using Assets.Source.Features.PlayerData;
using UniRx;
using UnityEngine;
using Zenject;
using Logger = Assets.Source.App.Logger;
using Random = UnityEngine.Random;

namespace Assets.Source.Entities.Items
{
    public class SpawnerEntity : AbstractMonoEntity
    {
        [SerializeField] private SpawningLanesConfig spawningLanesConfig;

        private AbstractItemEntity.Factory _itemEntityFactory;
        private JesterEntity _jesterEntity;
        private FlightStatsModel _flightStatsModel;

        protected int LastSpawnPoint = 0;
        protected int DistanceSinceLastSpawn = 0;

        protected float OffsetX;
        protected Vector3 GroundPosition;
        protected System.Random RandomPoolIndex;

        public override void Initialize()
        {
            RandomPoolIndex = new System.Random();
            OffsetX = Position.x - _jesterEntity.Position.x;
            GroundPosition = Position;

            _flightStatsModel.DistanceUnits
                .Where(_ => !_flightStatsModel.IsLanded.Value)
                .Subscribe(AttemptSpawn)
                .AddTo(this);

            Observable.EveryLateUpdate()
                .Subscribe(_ => OnLateUpdate())
                .AddTo(this);
        }

        [Inject]
        private void Inject(
            AbstractItemEntity.Factory itemEntityFactory,
            JesterEntity jesterEntity,
            FlightStatsModel flightStatsModel)
        {
            _itemEntityFactory = itemEntityFactory;
            _jesterEntity = jesterEntity;
            _flightStatsModel = flightStatsModel;
        }

        private void OnLateUpdate()
        {
            Vector3 targetPos = _jesterEntity.Position;

            Position = new Vector3(targetPos.x + OffsetX, Position.y, Position.z);
        }

        // Checks if Spawn should occur and Spawns object
        protected virtual void AttemptSpawn(float distance)
        {
            // Try a spawn in each of the lanes
            for (var i = 0; i < spawningLanesConfig.SpawningLanes.Count; i++)
            {
                SpawningLane spawningLane = spawningLanesConfig.SpawningLanes[i];

                if (ShouldSpawn((int)distance, spawningLane))
                {
                    SpawnRandomItem(spawningLane);
                }
            }
        }

        protected virtual bool ShouldSpawn(int distance, SpawningLane spawningLane)
        {
            // Sanity check
            if (spawningLane.ItemPool.Count == 0)
            {
                Logger.Warn("Item Pool is empty! Please verify if everything is correctly setup in the spawners");
                return false;
            }

            // Do not spawn if the jester is above the lane max height and is also moving upwards
            if (!spawningLanesConfig.SpawnOnTrajectory &&
                _jesterEntity.Position.y > spawningLane.MaxHeight &&
                _jesterEntity.GoBody.velocity.y > 0)
            {
                return false;
            }

            DistanceSinceLastSpawn = distance - LastSpawnPoint;

            // Do not spawn if the minimum distance since last Spawn was not travelled yet
            if (DistanceSinceLastSpawn <= spawningLanesConfig.MinDistanceBetweenSpawns)
            {
                return false;
            }

            // Randomly decide whether to spawn
            bool result = spawningLanesConfig.SpawnChance >= Random.Range(0.01f, 1.0f);

            if (result)
            {
                // Reset distance tracking
                LastSpawnPoint = distance;
                DistanceSinceLastSpawn = 0;
            }

            return result;
        }

        // Spawns a random item from the pool
        protected virtual void SpawnRandomItem(SpawningLane spawningLane)
        {
            var itemPool = spawningLane.ItemPool;
            int index = RandomPoolIndex.Next(0, itemPool.Count);

            var item = _itemEntityFactory.Create(itemPool[index]);

            item.Position = GetSpawnPosition(spawningLane);
            item.Initialize();
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
            float randomPosY = Random.Range(spawningLane.MinHeight, spawningLane.MaxHeight);

            // Introduce deviation on x position
            float deviationX = Random.Range(-spawningLanesConfig.MaxDeviation,
                spawningLanesConfig.MaxDeviation);

            var spawnPosition = new Vector2(Position.x + deviationX, GroundPosition.y + randomPosY);
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
            Vector2 jesterVelocity = _jesterEntity.GoBody.velocity;
            Vector2 jesterPos = _jesterEntity.Position;
            Vector2 v2Gravity = Physics.gravity;

            // The jester's travel-time from his current position to the Spawners X
            float time = OffsetX / jesterVelocity.x;

            // Calculate position on trajectory at the spawners X
            Vector2 projected = jesterPos + (jesterVelocity * time) + (v2Gravity * time * time * 0.5f);

            return projected;
        }
    }
}
