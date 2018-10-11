using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Items.Config
{
    [System.Serializable]
    public class SpawningLane
    {
        public float MinHeight;
        public float MaxHeight;
        public List<GameObject> ItemPool;

        public GameObject this[int key]
        {
            get { return ItemPool[key]; }
            set { ItemPool[key] = value; }
        }
    }
}
