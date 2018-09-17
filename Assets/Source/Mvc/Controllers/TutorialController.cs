using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : ClosableController
    {
        private readonly TutorialView _view;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly SavegameService _savegameService;

        public TutorialController(TutorialView view, SceneTransitionService sceneTransitionService, SavegameService savegameService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _sceneTransitionService = sceneTransitionService;
            _savegameService = savegameService;

            _view.OnNextClickedOnLastSlide
                .Subscribe(_ => OnNextClickedOnLastSlide())
                .AddTo(Disposer);
        }


        public void OnNextClickedOnLastSlide()
        {
            // ToDo handle SaveGame
            Close();           
        }
    }
}
