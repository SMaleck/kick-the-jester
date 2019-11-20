using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegames;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class ResetProfileConfirmationController : ClosableController
    {
        private readonly ResetProfileConfirmationView _view;
        private readonly SavegameService _savegameService;
        private readonly SceneTransitionService _sceneTransitionService;
        

        public ResetProfileConfirmationController(
            ResetProfileConfirmationView view,
            OpenPanelModel openPanelModel,
            SavegameService savegameService, 
            SceneTransitionService sceneTransitionService) 
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _savegameService = savegameService;
            _sceneTransitionService = sceneTransitionService;

            openPanelModel.OnOpenResetConfirmation
                .Subscribe(_ => Open())
                .AddTo(Disposer);

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
