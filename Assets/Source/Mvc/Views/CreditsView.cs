using System.Collections.Generic;
using Assets.Source.Services;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{   
    public class CreditsView : ClosableView
    {
        [Header("Panal Content")]
        [SerializeField] private TextMeshProUGUI _panelTitleText;
        [SerializeField] private List<GameObject> _defaultItems;
        [SerializeField] private List<GameObject> _webItems;

        [Header("Code Section")]
        [SerializeField] private TextMeshProUGUI _codeSectionHeader;
        [SerializeField] private Button smaleckButton;
        [SerializeField] private Button jhartmannButton;

        [Header("Art Section")]
        [SerializeField] private TextMeshProUGUI _artSectionHeader;
        [SerializeField] private Button jiubeckButton;

        [Header("Music Section")]
        [SerializeField] private TextMeshProUGUI _musicSectionHeader;
        [SerializeField] private Button tristanButton;

        [Header("Sound Effects Section")]
        [SerializeField] private TextMeshProUGUI _soundEffectsSectionHeader;
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

            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _panelTitleText.text = TextService.Credits();
            _codeSectionHeader.text = TextService.CreditsCodeHeader();
            _artSectionHeader.text = TextService.CreditsArtHeader();
            _musicSectionHeader.text = TextService.CreditsMusicHeader();
            _soundEffectsSectionHeader.text = TextService.CreditsSoundEffectsHeader();
        }

        private void SetupDefaultView()
        {
            SetAllIsActive(_defaultItems, true);
            SetAllIsActive(_webItems, false);

            smaleckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(smaleckkUrl)).AddTo(this);
            jhartmannButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jhartmannUrl)).AddTo(this);
            jiubeckButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(jiubeckUrl)).AddTo(this);

            tristanButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(tristanUrl)).AddTo(this);
            noiseForFunButton.OnClickAsObservable().Subscribe(_ => Application.OpenURL(noiseForFunUrl)).AddTo(this);
        }

        private void SetupWebView()
        {
            SetAllIsActive(_defaultItems, false);
            SetAllIsActive(_webItems, true);
        }

        private void SetAllIsActive(List<GameObject> items, bool isActive)
        {
            items.ForEach(item => item.SetActive(isActive));
        }
    }
}
