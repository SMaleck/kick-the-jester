using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class CreditsView : ClosableView
    {
        [SerializeField] private GameObject panelDefault;
        [SerializeField] private GameObject panelWeb;

        public override void Initialize()
        {
            base.Initialize();


            #if !UNITY_WEBGL

            panelDefault.SetActive(true);
            panelWeb.SetActive(false);
            
            #else

            panelDefault.SetActive(false);
            panelWeb.SetActive(true);

            #endif
        }
    }
}
