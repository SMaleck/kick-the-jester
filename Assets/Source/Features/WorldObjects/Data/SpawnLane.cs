using System;
using System.Collections.Generic;
using Assets.Source.Features.WorldObjects;
using UnityEngine;

namespace Assets.Source.Features.PickupItems.Data
{
    [Serializable]
    public class SpawnLane
    {
        [Tooltip("Overall probability, that this lane will spawn")]
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _laneSpawnChance = 0.8f;
        public float LaneSpawnChance => _laneSpawnChance;

        [Tooltip("TRUE = Item will be spawned on the Jester's projected trajectory")]
        [SerializeField] private bool _spawnOnTrajectory = true;
        public bool SpawnOnTrajectory => _spawnOnTrajectory;

        [Tooltip("Determines by how much the Spawn location can deviate from the projected position")]
        [Range(0.0f, 15)]
        [SerializeField] private float _maxDeviation = 1f;
        public float MaxDeviation => _maxDeviation;

        [Range(0, 1000)]
        [SerializeField] private int _minDistanceBetweenSpawnsUnits = 20;
        public int MinDistanceBetweenSpawnsUnits => _minDistanceBetweenSpawnsUnits;

        [SerializeField] private float _minHeightUnits;
        public float MinHeightUnits => _minHeightUnits;

        [SerializeField] private float _maxHeightUnits;
        public float MaxHeightUnits => _maxHeightUnits;

        [SerializeField] private List<WorldObjectType> _itemPool;
        public IReadOnlyList<WorldObjectType> ItemPool => _itemPool;
    }
}
