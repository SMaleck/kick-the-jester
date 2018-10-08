using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
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
            _view.Initialize();

            _model = model;
            _settingsModel = settingsModel;

            _view.IsMusicMuted
                .Subscribe(value => settingsModel.IsMusicMuted.Value = value)
                .AddTo(Disposer);

            _view.IsEffectsMuted
                .Subscribe(value => settingsModel.IsEffectsMuted.Value = value)
                .AddTo(Disposer);

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => settingsModel.RestoreDefaults())
                .AddTo(Disposer);

            _model.OpenSettings
                .Subscribe(_ => Open())
                .AddTo(Disposer);
        }        
        

        public override void Open()
        {
            _view.Open();
        }


        public override void Close()
        {
            _view.Close();
        }
    }
}
