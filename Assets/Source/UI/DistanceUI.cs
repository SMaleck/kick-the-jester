using Assets.Source.App;
using Assets.Source.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{  
    public class DistanceUI : MonoBehaviour
    {        
        public Text t_Distance;

        void Start()
        {
            // Register for Updates
            Singletons.gameStateManager.AttachForDistance(UpdateUI);
        }


        public void UpdateUI(int Value)
        {
            t_Distance.text = Value.ToString();
        }
    }
}