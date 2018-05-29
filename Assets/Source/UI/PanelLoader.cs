using Assets.Source.UI.Panels;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.UI
{
    public class PanelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Prefabs;

        private void Start()
        {
            foreach (GameObject prefab in Prefabs)
            {
                GameObject go = GameObject.Instantiate(prefab);
                go.transform.SetParent(gameObject.transform, false);

                go.GetComponent<AbstractPanel>().Setup();
            }
        }
    }
}
