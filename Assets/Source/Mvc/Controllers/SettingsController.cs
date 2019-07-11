using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class SettingsController : ClosableController
    {
        private readonly SettingsView _view;

        public SettingsController(
            SettingsView view,
            OpenPanelModel openPanelModel,
            SettingsModel settingsModel)
            : base(view)
        {
            _view = view;

            _view.IsMusicMuted = settingsModel.IsMusicMuted;
            _view.IsEffectsMuted = settingsModel.IsEffectsMuted;

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => settingsModel.RestoreDefaults())
                .AddTo(Disposer);

            _view.OnResetProfileClicked
                .Subscribe(_ => openPanelModel.OpenResetConfirmation())
                .AddTo(Disposer);

            openPanelModel.OnOpenSettings
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _view.Initialize();
        }        
    }
}
