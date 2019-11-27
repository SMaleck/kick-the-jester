using Assets.Source.Util;
using Zenject;

namespace Assets.Source.Mvc.Mediation
{
    public class ClosableViewController : AbstractDisposable, IClosableViewController
    {
        public class Factory : PlaceholderFactory<IClosableView, ClosableViewController> { }

        private readonly IClosableView _closableView;

        public ClosableViewController(IClosableView closableView)
        {
            _closableView = closableView;
        }

        public void Open()
        {
            _closableView.Open();
        }

        public void Close()
        {
            _closableView.Close();
        }
    }
}
