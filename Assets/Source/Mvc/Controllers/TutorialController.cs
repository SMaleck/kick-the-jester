using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : ClosableController
    {
        private readonly TutorialView _view;
        private readonly TitleModel _titleModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public TutorialController(
            TutorialView view,
            TitleModel titleModel,
            OpenPanelModel openPanelModel,
            SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _titleModel = titleModel;
            _view.Initialize();

            _sceneTransitionService = sceneTransitionService;

            _view.OnNextClickedOnLastSlide
                .Subscribe(_ => OnNextClickedOnLastSlide())
                .AddTo(Disposer);

            openPanelModel.OnOpenTutorial
                .Subscribe(_ => Open())
                .AddTo(Disposer);
        }


        public void OnNextClickedOnLastSlide()
        {
            if (_titleModel.IsFirstStart.Value)
            {
                _titleModel.IsFirstStart.Value = false;
                _sceneTransitionService.ToGame();
                return;
            }

            Close();
        }
    }
}
