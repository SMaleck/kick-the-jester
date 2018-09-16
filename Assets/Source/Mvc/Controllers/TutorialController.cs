using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : AbstractController
    {
        private readonly TutorialView _view;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly SavegameService _savegameService;

        public TutorialController(TutorialView view, SceneTransitionService sceneTransitionService, SavegameService savegameService)
        {
            _view = view;
            _sceneTransitionService = sceneTransitionService;
            _savegameService = savegameService;

            _view.OnNextClickedOnLastSlide
                .Subscribe(_ => OnNextClickedOnLastSlide())
                .AddTo(Disposer);
        }

        public override void Open()
        {
            _view.Open();
        }

        public void OnNextClickedOnLastSlide()
        {
            // ToDo handle SaveGame
            _view.Close();
        }
    }
}
