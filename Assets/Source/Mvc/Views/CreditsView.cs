using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{   
    public class CreditsView : ClosableView
    {
        [SerializeField] private GameObject panelDefault;
        [SerializeField] private GameObject panelWeb;

        [SerializeField] private Button smaleckButton;
        [SerializeField] private Button jhartmannButton;
        [SerializeField] private Button jiubeckButton;

        [SerializeField] private Button tristanButton;
        [SerializeField] private Button noiseForFunButton;

        private readonly string smaleckkUrl = "https://github.com/SMaleck";
        private readonly string jhartmannUrl = "https://github.com/jonashartmann";
        private readonly string jiubeckUrl = "https://jiubeck.deviantart.com/";

        private readonly string tristanUrl = "https://www.tristanlohengrin.fr/";
        private readonly string noiseForFunUrl = "http://www.noiseforfun.com/";

        
        public override void Setup()
        {
            base.Setup();

            #if !UNITY_WEBGL

            SetupDefaultView();
            
            #else

            SetupWebView();

            #endif
        }

        private void SetupDefaultView()
        {
            panelDefault.SetActive(true);
            panelWeb.SetActive(false);

            smaleckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(smaleckkUrl)).AddTo(this);
            jhartmannButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jhartmannUrl)).AddTo(this);
            jiubeckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jiubeckUrl)).AddTo(this);

            tristanButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(tristanUrl)).AddTo(this);
            noiseForFunButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(noiseForFunUrl)).AddTo(this);
        }

        private void SetupWebView()
        {
            panelDefault.SetActive(false);
            panelWeb.SetActive(true);
        }
    }
}
