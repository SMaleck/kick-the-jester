using Assets.Source.App;
using Assets.Source.Mvc.Views;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Savegame;
using SceneTransitionService = Assets.Source.Services.SceneTransitionService;

namespace Assets.Source.Mvc.Controllers
{
    public class TitleController : AbstractController
    {
        private readonly TitleView _view;
        private readonly SettingsController _settingsController;
        private readonly CreditsController _creditsController;
        private readonly TutorialController _tutorialController;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;
        private readonly SavegameService _savegameService;

        public TitleController(TitleView view, SettingsController settingsController, CreditsController creditsController, TutorialController tutorialController,
            SceneTransitionService sceneTransitionService, AudioService audioService, SavegameService savegameService)
        {
            _view = view;
            _settingsController = settingsController;
            _creditsController = creditsController;
            _tutorialController = tutorialController;
            _sceneTransitionService = sceneTransitionService;
            _audioService = audioService;
            _savegameService = savegameService;

            _view.Initialize();

            _view.OnStartClicked = () =>
            {
                _sceneTransitionService.ToGame();                
            };

            _view.OnSettingsClicked = () => { _settingsController.Open(); };
            _view.OnCreditsClicked = () => { _creditsController.Open(); };
            _view.OnTutorialClicked = () => { _tutorialController.Open(); };            
        }
    }
}