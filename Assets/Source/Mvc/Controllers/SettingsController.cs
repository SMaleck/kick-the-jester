using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class SettingsController : ClosableController
    {
        private readonly SettingsView _view;
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsView view, SettingsService settingsService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

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

            _view.OnRestoreDefaultsClicked = () => { _settingsService.RestoreDefaults(); };
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
