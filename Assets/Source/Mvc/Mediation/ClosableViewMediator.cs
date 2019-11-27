using System.Collections.Generic;

namespace Assets.Source.Mvc.Mediation
{
    public class ClosableViewMediator : IClosableViewMediator, IClosableViewRegistrar
    {
        private readonly Dictionary<ClosableViewType, IClosableViewController> _closableViewControllers;

        public ClosableViewMediator()
        {
            _closableViewControllers = new Dictionary<ClosableViewType, IClosableViewController>();
        }

        void IClosableViewRegistrar.RegisterClosableView(
            ClosableViewType closableViewType,
            IClosableViewController closableViewController)
        {
            _closableViewControllers.Add(closableViewType, closableViewController);
        }

        public void Open(ClosableViewType closableViewType)
        {
            if (_closableViewControllers.TryGetValue(closableViewType, out var controller))
            {
                controller.Open();
            }
        }

        public void Close(ClosableViewType closableViewType)
        {
            if (_closableViewControllers.TryGetValue(closableViewType, out var controller))
            {
                controller.Close();
            }
        }
    }
}
