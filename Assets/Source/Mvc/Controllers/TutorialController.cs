using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : ClosableController
    {
        private readonly TutorialView _view;
        private readonly TitleModel _model;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly SavegameService _savegameService;

        public TutorialController(TutorialView view, TitleModel model, SceneTransitionService sceneTransitionService, SavegameService savegameService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _sceneTransitionService = sceneTransitionService;
            _savegameService = savegameService;

            _view.OnNextClickedOnLastSlide
                .Subscribe(_ => OnNextClickedOnLastSlide())
                .AddTo(Disposer);

            _model = model;
            _model.OpenTutorial
                .Subscribe(_ => Open())
                .AddTo(Disposer);
        }


        public void OnNextClickedOnLastSlide()
        {
            if (_model.IsFirstStart.Value)
            {
                _model.IsFirstStart.Value = false;
                _sceneTransitionService.ToGame();
                return;
            }
            
            Close();           
        }
    }
}
