using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    class PauseController : ClosableController
    {
        private readonly PauseView _view;
        private readonly GameStateModel _gameStateModel;
        private readonly SettingsModel _settingsModel;
        private readonly UserInputModel _userInputModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public PauseController(PauseView view, GameStateModel gameStateModel, SettingsModel settingsModel, UserInputModel userInputModel, SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            
            _gameStateModel = gameStateModel;
            _settingsModel = settingsModel;
            _userInputModel = userInputModel;
            _sceneTransitionService = sceneTransitionService;

            _gameStateModel.IsPaused
                .Subscribe(OnPauseChanged)
                .AddTo(Disposer);

            _userInputModel.OnPause
                .Subscribe(_ => OnUserInputPause())
                .AddTo(Disposer);

            _view.IsMusicMuted = _settingsModel.IsMusicMuted;
            _view.IsEffectsMuted = _settingsModel.IsEffectsMuted;

            _view.OnCloseCompleted
                .Subscribe(_ => _gameStateModel.IsPaused.Value = false)
                .AddTo(Disposer);

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
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

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }
    }
}
