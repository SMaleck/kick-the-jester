using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class PrefabLoader : AbstractBehaviour
    {
        [SerializeField] List<GameObject> Prefabs;


        private void Awake()
        {
            foreach (GameObject prefab in Prefabs)
            {
                GameObject go = GameObject.Instantiate(prefab);
                go.transform.SetParent(goTransform);
                go.transform.localScale = new Vector3(1, 1, 1);
                // Ensures that everything works smoothly even when Prefabs are stored "inactive"
                go.SetActive(true);
            }
        }
    }
}
