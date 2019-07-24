using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class AbstractController : AbstractDisposable
    {
        protected readonly AbstractView View;

        protected AbstractController(AbstractView view)
        {
            View = view;
            this.AddTo(view);
        }
    }
}
