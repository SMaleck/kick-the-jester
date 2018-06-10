﻿using Assets.Source.UI.Panels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.UI
{
    public class PanelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Prefabs;
        public List<AbstractPanel> Panels { get; private set; }

        private void Start()
        {
            Panels = new List<AbstractPanel>();

            foreach (GameObject prefab in Prefabs)
            {
                GameObject go = GameObject.Instantiate(prefab);
                go.transform.SetParent(gameObject.transform, false);
                go.name = prefab.name;

                Panels.Add(go.GetComponent<AbstractPanel>());
                Panels.Last().Setup();                
            }
        }
    }
}
