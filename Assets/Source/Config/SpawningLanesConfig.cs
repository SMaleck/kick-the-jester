using Assets.Source.Behaviours.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Config
{
    public class SpawningLanesConfig : ScriptableObject
    {
        public List<SpawningLane> SpawningLanes = new List<SpawningLane>();

        // Percent-based Spawn chance
        [Range(0.0f, 1.0f)]
        public float SpawnChance = 0.8f;

        // If this is set, item will be spanwed on the Jesters projected trajectory
        public bool SpawnOnTrajectory = true;

        // Determines by how much the Spawn location can deviate from the projected position
        [Range(0.0f, 15)]
        public float MaxDeviation = 1f;

        [Range(0, 1000)]
        [Tooltip("In Units")]
        public int MinDistanceBetweenSpawns = 20;

        public float MinHeight = 0f;

    }
}
