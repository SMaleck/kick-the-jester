using Assets.Source.Services.Localization;
using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class SettingsView : AbstractView
    {
        private const string MutedIcon = "\uf6a9";
        private const string UnmutedIcon = "\uf028";

        [SerializeField] private TextMeshProUGUI _panelTitleText;

        [Header("Sound Settings")]
        [SerializeField] private TextMeshProUGUI _soundSettingsTitleText;

        [SerializeField] private Button _isMusicMutedButton;
        [SerializeField] private TextMeshProUGUI _isMusicMutedButtonText;
        [SerializeField] private TextMeshProUGUI _isMusicMutedText;

        [SerializeField] private Button _isEffectsMutedButton;
        [SerializeField] private TextMeshProUGUI _isEffectsMutedButtonText;
        [SerializeField] private TextMeshProUGUI _isEffectsMutedText;

        [Header("Language Settings")]
        [SerializeField] private TextMeshProUGUI _languageSettingsTitleText;

        [SerializeField] private Button _englishButton;
        [SerializeField] private TextMeshProUGUI _englishButtonText;

        [SerializeField] private Button _germanButton;
        [SerializeField] private TextMeshProUGUI _germanButtonText;

        [Header("Reset Profile")]
        [SerializeField] private Button _resetProfileButton;
        [SerializeField] private TextMeshProUGUI _resetProfileButtonText;

        [Header("Restore Defaults")]
        [SerializeField] private Button _restoreDefaultsButton;
        [SerializeField] private TextMeshProUGUI _restoreDefaultsButtonText;


        private readonly Subject<Unit> _onMuteMusicToggled = new Subject<Unit>();
        public IObservable<Unit> OnMuteMusicToggled => _onMuteMusicToggled;

        private readonly Subject<Unit> _onMuteSoundToggled = new Subject<Unit>();
        public IObservable<Unit> OnMuteSoundToggled => _onMuteSoundToggled;


        private readonly Subject<Language> _onLanguageSelected = new Subject<Language>();
        public IObservable<Language> OnLanguageSelected => _onLanguageSelected;


        private readonly ReactiveCommand _onRestoreDefaultsClicked = new ReactiveCommand();
        public IObservable<Unit> OnRestoreDefaultsClicked => _onRestoreDefaultsClicked;

        private readonly ReactiveCommand _onResetProfileClicked = new ReactiveCommand();
        public IObservable<Unit> OnResetProfileClicked => _onResetProfileClicked;

        public override void Setup()
        {
            _onMuteMusicToggled.AddTo(Disposer);

            _isMusicMutedButton.OnClickAsObservable()
                .Subscribe(_ => _onMuteMusicToggled.OnNext(Unit.Default))
                .AddTo(Disposer);

            _onMuteSoundToggled.AddTo(Disposer);
            _isEffectsMutedButton.OnClickAsObservable()
                .Subscribe(_ => _onMuteSoundToggled.OnNext(Unit.Default))
                .AddTo(Disposer);


            _onLanguageSelected.AddTo(Disposer);

            _englishButton.OnClickAsObservable()
                .Subscribe(_ => _onLanguageSelected.OnNext(Language.English))
                .AddTo(Disposer);

            _germanButton.OnClickAsObservable()
                .Subscribe(_ => _onLanguageSelected.OnNext(Language.German))
                .AddTo(Disposer);


            _onRestoreDefaultsClicked.AddTo(Disposer);
            _onRestoreDefaultsClicked.BindTo(_restoreDefaultsButton).AddTo(Disposer);

            _onResetProfileClicked.AddTo(Disposer);
            _onResetProfileClicked.BindTo(_resetProfileButton).AddTo(Disposer);

            SetLanguageButtonsInteractable();
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            _panelTitleText.text = TextService.Settings();

            _soundSettingsTitleText.text = TextService.SoundSettings();
            _isMusicMutedText.text = TextService.Music();
            _isEffectsMutedText.text = TextService.SoundEffects();

            _languageSettingsTitleText.text = TextService.Language();
            _englishButtonText.text = TextService.LanguageName(Language.English);
            _germanButtonText.text = TextService.LanguageName(Language.German);

            _restoreDefaultsButtonText.text = TextService.RestoreDefaults();
            _resetProfileButtonText.text = TextService.ResetProfile();
        }

        private void SetLanguageButtonsInteractable()
        {
            switch (TextService.CurrentLanguage)
            {
                case Language.English:
                    _englishButton.interactable = false;
                    _germanButton.interactable = true;
                    break;
                case Language.German:
                    _englishButton.interactable = true;
                    _germanButton.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetIsMusicMuted(bool isMusicMuted)
        {
            _isMusicMutedButtonText.text = isMusicMuted ? MutedIcon : UnmutedIcon;
        }

        public void SetIsSoundMuted(bool isSoundMuted)
        {
            _isEffectsMutedButtonText.text = isSoundMuted ? MutedIcon : UnmutedIcon;
        }
    }
}
