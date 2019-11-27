using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegames;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class ResetProfileConfirmationController : AbstractDisposable
    {
        private readonly ResetProfileConfirmationView _view;
        private readonly ISavegameService _savegameService;
        private readonly SceneTransitionService _sceneTransitionService;


        public ResetProfileConfirmationController(
            ResetProfileConfirmationView view,
            ISavegameService savegameService,
            SceneTransitionService sceneTransitionService)
        {
            _view = view;

            _savegameService = savegameService;
            _sceneTransitionService = sceneTransitionService;

            _view.OnResetConfirmClicked
                .Subscribe(_ => OnResetConfirmed())
                .AddTo(Disposer);
        }

        private void OnResetConfirmed()
        {
            _savegameService.Reset();
            _sceneTransitionService.ToTitle();
        }
    }
}
