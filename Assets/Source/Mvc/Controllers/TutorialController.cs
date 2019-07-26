using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : ClosableController
    {
        private readonly TutorialView _view;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public TutorialController(
            TutorialView view,
            PlayerProfileModel playerProfileModel,
            OpenPanelModel openPanelModel,
            SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _playerProfileModel = playerProfileModel;
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
            if (_playerProfileModel.HasCompletedTutorial.Value)
            {
                _playerProfileModel.SetHasCompletedTutorial(true);
                _sceneTransitionService.ToGame();
                return;
            }

            Close();
        }
    }
}
