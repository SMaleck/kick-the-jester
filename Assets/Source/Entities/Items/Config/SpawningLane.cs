using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Items.Config
{
    [System.Serializable]
    public class SpawningLane
    {
        public float minHeight;
        public float maxHeight;
        public List<GameObject> itemPool;

        public GameObject this[int key]
        {
            get { return itemPool[key]; }
            set { itemPool[key] = value; }
        }
    }
}
