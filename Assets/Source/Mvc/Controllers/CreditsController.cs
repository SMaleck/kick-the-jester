using Assets.Source.Mvc.Views;

namespace Assets.Source.Mvc.Controllers
{
    public class CreditsController : ClosableController
    {
        private readonly CreditsView _view;

        public CreditsController(CreditsView view)
            : base(view)
        {
            _view = view;
            _view.Initialize();
        }
    }
}
