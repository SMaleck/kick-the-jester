using Assets.Source.UI.Panels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

namespace Assets.Source.UI
{
    public class PanelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> prefabs;
        private AbstractPanel.Factory panelFactory;

        public Canvas ParentCanvas { get; private set; }
        public List<AbstractPanel> Panels { get; private set; }

        [Inject]
        public void Init(AbstractPanel.Factory panelFactory)
        {
            this.panelFactory = panelFactory;
        }

        private void Start()
        {
            Panels = new List<AbstractPanel>();
            ParentCanvas = GetComponent<Canvas>();
            
            foreach (GameObject prefab in prefabs)
            {
                CreatePanelFromPrefab(prefab);
            }

        }

        private void CreatePanelFromPrefab(GameObject prefab)
        {
            AbstractPanel panel = panelFactory.Create(prefab);
            panel.gameObject.transform.SetParent(gameObject.transform, false);
            panel.gameObject.name = prefab.name;

            Panels.Add(panel);
            panel.Setup();
        }
    }
}
