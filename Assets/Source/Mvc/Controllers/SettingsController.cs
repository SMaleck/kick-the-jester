using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class SettingsController : ClosableController
    {
        private readonly SettingsView _view;
        private readonly TitleModel _model;
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsView view, TitleModel model, SettingsService settingsService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _model = model;
            _settingsService = settingsService;            

            _settingsService.MusicVolume
                .Subscribe(e => _view.MusicVolume.Value = e)
                .AddTo(Disposer);

            _view.MusicVolume
                .Subscribe(e => _settingsService.MusicVolume.Value = e)
                .AddTo(Disposer);

            _settingsService.EffectsVolume
                .Subscribe(e => _view.EffectsVolume.Value = e)
                .AddTo(Disposer);

            _view.EffectsVolume
                .Subscribe(e => _settingsService.EffectsVolume.Value = e)
                .AddTo(Disposer);

            _view.OnRestoreDefaultsClicked
                .Subscribe(_ => _settingsService.RestoreDefaults())
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
