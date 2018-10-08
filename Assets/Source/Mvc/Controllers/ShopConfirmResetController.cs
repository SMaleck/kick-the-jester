using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class ShopConfirmResetController : ClosableController
    {
        private ShopConfirmResetView _view;
        private ShopModel _shopModel;

        public ShopConfirmResetController(ShopConfirmResetView view, ShopModel shopModel) 
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _shopModel = shopModel;

            _shopModel.OpenConfirmReset
                .Subscribe(_ => Open())
                .AddTo(Disposer);

            _view.OnResetConfirmClicked
                .Subscribe(_ => OnResetConfirmed())
                .AddTo(Disposer);
        }

        private void OnResetConfirmed()
        {
            // ToDo Implement reset
        }
    }
}
