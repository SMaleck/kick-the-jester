using Assets.Source.Mvc.Views;

namespace Assets.Source.Mvc.Controllers
{
    public class ClosableController : AbstractController
    {
        private ClosableView _view;
        
        public ClosableController(ClosableView view)
        {
            _view = view;            
        }

        public virtual void Open()
        {
            _view.Open();
        }

        public virtual void Close()
        {
            _view.Close();
        }
    }
}
