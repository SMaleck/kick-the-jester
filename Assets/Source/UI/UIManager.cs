using Assets.Source.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI
{  
    public class UIManager : MonoBehaviour
    {        
        public Text t_Distance;

        public KickForceUI kickForceUI { get; private set; }


        public void Register<T>(T uiComponent) where T : BaseComponent
        {
            if(uiComponent is KickForceUI)
            {
                kickForceUI = uiComponent as KickForceUI;
            }
        }

        /* ---------------------------------------------------------------- */
        #region Public Interface


        public void UpdateDistance(object Value)
        {
            t_Distance.text = Value.ToString();
        }

        #endregion
    }
}