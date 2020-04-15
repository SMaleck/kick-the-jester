using Assets.Source.App;
using Assets.Source.Features.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Features.PickupItems.Data
{
    [CreateAssetMenu(fileName = nameof(PickUpItemSpawnConfig), menuName = Constants.UMenuRoot + nameof(PickUpItemSpawnConfig))]
    public class PickUpItemSpawnConfig : ScriptableObject, IPickUpItemSpawnData
    {
        [Serializable]
        private class PickUpItemPrefab
        {
            [SerializeField] private WorldObjectType _worldObjectType;
            public WorldObjectType WorldObjectType => _worldObjectType;

            [SerializeField] private UnityEngine.Object _prefab;
            public UnityEngine.Object Prefab => _prefab;
        }

        [SerializeField] private List<PickUpItemPrefab> _pickUpItemPrefabs;

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
