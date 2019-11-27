using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TutorialController : AbstractDisposable
    {
        private readonly TutorialView _view;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly IClosableViewMediator _closableViewMediator;

        public TutorialController(
            TutorialView view,
            PlayerProfileModel playerProfileModel,
            SceneTransitionService sceneTransitionService,
            IClosableViewMediator closableViewMediator)
        {
            _view = view;
            _playerProfileModel = playerProfileModel;

            _sceneTransitionService = sceneTransitionService;
            _closableViewMediator = closableViewMediator;

            _view.OnNextClickedOnLastSlide
                .Subscribe(_ => OnNextClickedOnLastSlide())
                .AddTo(Disposer);
        }


        public void OnNextClickedOnLastSlide()
        {
            if (!_playerProfileModel.HasCompletedTutorial.Value)
            {
                _playerProfileModel.SetHasCompletedTutorial(true);
                _sceneTransitionService.ToGame();
                return;
            }

            _closableViewMediator.Close(ClosableViewType.Tutorial);
        }
    }
}
