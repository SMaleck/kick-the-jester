using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Localization;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class SettingsController : AbstractDisposable
    {
        private readonly SettingsView _view;
        private readonly SceneTransitionService _sceneTransitionService;

        public SettingsController(
            SettingsView view,
            SettingsModel settingsModel,
            SceneTransitionService sceneTransitionService,
            IClosableViewMediator closableViewMediator)
        {
            _view = view;

            _sceneTransitionService = sceneTransitionService;

            settingsModel.IsMusicMuted
                .Subscribe(_view.SetIsMusicMuted)
                .AddTo(Disposer);

            settingsModel.IsSoundMuted
                .Subscribe(_view.SetIsSoundMuted)
                .AddTo(Disposer);

            _view.OnMuteMusicToggled
                .Where(value => value != settingsModel.IsMusicMuted.Value)
                .Subscribe(settingsModel.SetIsMusicMuted)
                .AddTo(Disposer);

            _view.OnMuteSoundToggled
                .Where(value => value != settingsModel.IsSoundMuted.Value)
                .Subscribe(settingsModel.SetIsEffectsMuted)
                .AddTo(Disposer);

            _view.OnLanguageSelected
                .Subscribe(SwitchLanguage)
                .AddTo(Disposer);

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => settingsModel.RestoreDefaults())
                .AddTo(Disposer);

            _view.OnResetProfileClicked
                .Subscribe(_ => closableViewMediator.Open(ClosableViewType.ResetProfileConfirmation))
                .AddTo(Disposer);
        }

        private void SwitchLanguage(Language language)
        {
            TextService.SetLanguage(language);
            _sceneTransitionService.ToTitle();
        }
    }
}
