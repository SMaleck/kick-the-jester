using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class CreditsController : ClosableController
    {
        private readonly CreditsView _view;
        private readonly TitleModel _model;

        public CreditsController(CreditsView view, TitleModel model)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _model = model;
            _model.OpenCredits
                .Subscribe(_ => Open())
                .AddTo(Disposer);
        }
    }
}
