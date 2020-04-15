using Assets.Source.App;
using Assets.Source.Features.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Features.PickupItems.Data
{
    [CreateAssetMenu(fileName = nameof(WorldObjectSpawnConfig), menuName = Constants.UMenuRoot + nameof(WorldObjectSpawnConfig))]
    public class WorldObjectSpawnConfig : ScriptableObject, IWorldObjectSpawnData
    {
        [Serializable]
        private class WorldObjectPrefab
        {
            [SerializeField] private WorldObjectType _worldObjectType;
            public WorldObjectType WorldObjectType => _worldObjectType;

            [SerializeField] private UnityEngine.Object _prefab;
            public UnityEngine.Object Prefab => _prefab;
        }

        [SerializeField] private List<WorldObjectPrefab> _pickUpItemPrefabs;

        [SerializeField] private List<SpawnLane> _spawnLanes;
        public IReadOnlyList<SpawnLane> SpawnLanes => _spawnLanes;

        public UnityEngine.Object GetPrefab(WorldObjectType worldObjectType)
        {
            return _pickUpItemPrefabs
                .First(prefab => prefab.WorldObjectType == worldObjectType)
                .Prefab;
        }
    }
}
