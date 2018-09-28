using Assets.Source.Mvc.Views;
using Assets.Source.Util;

// ToDo Make this inheritance more meaningful, e.g. controller<T>
namespace Assets.Source.Mvc.Controllers
{
    public class AbstractController : AbstractDisposable
    {
        protected readonly AbstractView View;

        protected AbstractController(AbstractView view)
        {
            View = view;            
        }
    }
}
