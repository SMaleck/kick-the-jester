using Assets.Source.Features.GameState;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class PauseController : ClosableController
    {
        private readonly PauseView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly UserInputModel _userInputModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public PauseController(
            PauseView view, 
            GameStateModel gameStateModel,
            UserInputModel userInputModel, 
            SceneTransitionService sceneTransitionService,
            OpenPanelModel openPanelModel)
            : base(view)
        {
            _view = view;
            
            _gameStateModel = gameStateModel;
            _userInputModel = userInputModel;
            _sceneTransitionService = sceneTransitionService;

            _gameStateModel.IsPaused
                .Subscribe(OnPauseChanged)
                .AddTo(Disposer);

            _userInputModel.OnPause
                .Subscribe(_ => OnUserInputPause())
                .AddTo(Disposer);

            _view.OnSettingsClicked
                .Subscribe(_ => openPanelModel.OpenSettings())
                .AddTo(Disposer);

            _view.OnRetryClicked
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);

            _view.OnCloseCompleted
                .Subscribe(_ => _gameStateModel.SetIsPaused(false))
                .AddTo(Disposer);

            _view.Initialize();
        }        

        private void OnUserInputPause()
        {
            if (_view.IsOpen)
            {
                Close();
                return;
            }

            Open();
        }

        private void OnPauseChanged(bool isPaused)
        {
            if (isPaused)
            {
                Open();
            }
        }
    }
}
