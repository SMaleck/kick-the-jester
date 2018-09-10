using Assets.Source.Services;

namespace Assets.Source.Mvc
{
    public class TitleController : AbstractController
    {
        private readonly TitleView _view;
        private readonly SettingsController _settingsController;
        private readonly SceneTransitionService _sceneTransitionService;

        public TitleController(TitleView view, SettingsController settingsController, SceneTransitionService sceneTransitionService)
        {
            _view = view;
            _settingsController = settingsController;
            _sceneTransitionService = sceneTransitionService;

            _view.Initialize();
            _view.OnStartClicked = () => { _sceneTransitionService.ToGame();};
            _view.OnSettingsClicked = () => { _settingsController.Open(); };
        }
    }
}
