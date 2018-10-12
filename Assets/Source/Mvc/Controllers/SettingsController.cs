using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class SettingsController : ClosableController
    {
        private readonly SettingsView _view;
        private readonly TitleModel _model;
        private readonly SettingsModel _settingsModel;

        public SettingsController(SettingsView view, TitleModel model, SettingsModel settingsModel)
            : base(view)
        {
            _view = view;
            _model = model;
            _settingsModel = settingsModel;

            _view.IsMusicMuted = _settingsModel.IsMusicMuted;
            _view.IsEffectsMuted = _settingsModel.IsEffectsMuted;

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => settingsModel.RestoreDefaults())
                .AddTo(Disposer);

            _model.OpenSettings
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            
            _view.Initialize();
        }        
    }
}
