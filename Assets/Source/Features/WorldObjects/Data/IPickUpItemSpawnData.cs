using System.Collections.Generic;
using Assets.Source.Features.WorldObjects;

namespace Assets.Source.Features.PickupItems.Data
{
    public interface IPickUpItemSpawnData
    {
        IReadOnlyList<SpawnLane> SpawnLanes { get; }
        UnityEngine.Object GetPrefab(WorldObjectType worldObjectType);
    }
}
