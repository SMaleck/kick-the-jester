using Assets.Source.Features.GameState;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class PauseController : AbstractDisposable
    {
        private readonly PauseView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly UserInputModel _userInputModel;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly IClosableViewMediator _closableViewMediator;

        public PauseController(
            PauseView view,
            GameStateModel gameStateModel,
            UserInputModel userInputModel,
            SceneTransitionService sceneTransitionService,
            IClosableViewMediator closableViewMediator)
        {
            _view = view;

            _gameStateModel = gameStateModel;
            _userInputModel = userInputModel;
            _sceneTransitionService = sceneTransitionService;
            _closableViewMediator = closableViewMediator;

            _gameStateModel.IsPaused
                .Subscribe(OnPauseChanged)
                .AddTo(Disposer);

            _userInputModel.OnPause
                .Subscribe(_ => OnUserInputPause())
                .AddTo(Disposer);

            _view.OnSettingsClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Settings))
                .AddTo(Disposer);

            _view.OnRetryClicked
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            _view.OnAchievementsClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Achievements))
                .AddTo(Disposer);

            _closableViewMediator.OnViewClosed
                .Where(viewType => viewType == ClosableViewType.Pause)
                .Subscribe(_ => _gameStateModel.SetIsPaused(false))
                .AddTo(Disposer);
        }

        private void OnUserInputPause()
        {
            if (_closableViewMediator.IsViewOpen(ClosableViewType.Pause))
            {
                _closableViewMediator.Close(ClosableViewType.Pause);
                return;
            }

            _closableViewMediator.Open(ClosableViewType.Pause);
        }

        private void OnPauseChanged(bool isPaused)
        {
            if (isPaused)
            {
                _closableViewMediator.Open(ClosableViewType.Pause);
            }
        }
    }
}
