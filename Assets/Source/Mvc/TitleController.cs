using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;

namespace Assets.Source.Mvc
{
    public class TitleController : AbstractController
    {
        private readonly TitleView _view;
        private readonly SettingsController _settingsController;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;

        public TitleController(TitleView view, SettingsController settingsController, SceneTransitionService sceneTransitionService, AudioService audioService)
        {
            _view = view;
            _settingsController = settingsController;
            _sceneTransitionService = sceneTransitionService;
            _audioService = audioService;

            _view.Initialize();
            _view.OnStartClicked = () => { _sceneTransitionService.ToGame();};
            _view.OnSettingsClicked = () => { _settingsController.Open(); };
        }
    }
}