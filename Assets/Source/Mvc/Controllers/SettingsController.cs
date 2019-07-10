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
            TitleModel titleModel, 
            SettingsModel settingsModel)
            : base(view)
        {
            _view = view;

            _view.IsMusicMuted = settingsModel.IsMusicMuted;
            _view.IsEffectsMuted = settingsModel.IsEffectsMuted;

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => settingsModel.RestoreDefaults())
                .AddTo(Disposer);

            titleModel.OpenSettings
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _view.Initialize();
        }        
    }
}
