using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class ShopConfirmResetController : ClosableController
    {
        private readonly ShopConfirmResetView _view;
        private readonly ShopModel _shopModel;
        private readonly SavegameService _savegameService;
        private readonly SceneTransitionService _sceneTransitionService;
        

        public ShopConfirmResetController(ShopConfirmResetView view, ShopModel shopModel, SavegameService savegameService, SceneTransitionService sceneTransitionService) 
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _shopModel = shopModel;
            _savegameService = savegameService;
            _sceneTransitionService = sceneTransitionService;

            _shopModel.OpenConfirmReset
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _view.OnResetConfirmClicked
                .Subscribe(_ => OnResetConfirmed())
                .AddTo(Disposer);
        }

        private void OnResetConfirmed()
        {            
            _savegameService.Reset();
            _sceneTransitionService.ToGame();
        }
    }
}
