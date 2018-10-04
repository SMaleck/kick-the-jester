using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    class PauseController : ClosableController
    {
        private readonly PauseView _view;
        private readonly SettingsModel _settingsModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public PauseController(PauseView view, SettingsModel settingsModel, SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _settingsModel = settingsModel;
            _sceneTransitionService = sceneTransitionService;

            _view.IsMusicMuted
                .Subscribe(value => _settingsModel.IsMusicMuted.Value = value)
                .AddTo(Disposer);

            _view.IsEffectsMuted
                .Subscribe(value => _settingsModel.IsEffectsMuted.Value = value)
                .AddTo(Disposer);

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
                .AddTo(Disposer);
        }

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }
    }
}
