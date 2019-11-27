using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class CreditsController : ClosableController
    {
        private readonly CreditsView _view;

        public CreditsController(
            CreditsView view,
            OpenPanelModel openPanelModel)
            : base(view)
        {
            _view = view;

            openPanelModel.OnOpenCredits
                .Subscribe(_ => Open())
                .AddTo(Disposer);
        }
    }
}
