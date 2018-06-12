using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class CreditsPanel : AbstractPanel
    {
        [Header("Panel Properties")]
        [SerializeField] private Button closeButton;

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

            closeButton.OnClickAsObservable().Subscribe(_ => Hide()).AddTo(this);

            smaleckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(smaleckkUrl)).AddTo(this);
            jhartmannButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jhartmannUrl)).AddTo(this);
            jiubeckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jiubeckUrl)).AddTo(this);

            tristanButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(tristanUrl)).AddTo(this);
            noiseForFunButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(noiseForFunUrl)).AddTo(this);
        }
    }
}
