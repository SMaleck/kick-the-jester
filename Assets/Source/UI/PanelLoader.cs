using Assets.Source.UI.Panels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.UI
{
    public class PanelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> Prefabs;
        
        public Canvas ParentCanvas { get; private set; }
        public List<AbstractPanel> Panels { get; private set; }

        
        private void Start()
        {
            Panels = new List<AbstractPanel>();
            ParentCanvas = GetComponent<Canvas>();

            foreach (GameObject prefab in Prefabs)
            {
                GameObject go = GameObject.Instantiate(prefab, gameObject.transform, false);
                
                go.transform.SetParent(gameObject.transform, false);
                go.name = prefab.name;

                Panels.Add(go.GetComponent<AbstractPanel>());

                App.Cache.Kernel.MainContainer.Inject(go.GetComponent<AbstractPanel>());
                
                Panels.Last().Setup();                
            }
        }
    }
}
