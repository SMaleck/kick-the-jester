using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using System.Linq;
using Zenject;

namespace Assets.Source.App.Initialization
{
    public class AbstractSceneInitializer
    {
        [Inject] private readonly IClosableViewRegistrar _closableViewRegistrar;
        [Inject] private readonly ClosableViewController.Factory _closableViewControllerFactory;

        protected void SetupView<T>(T view) where T : AbstractView
        {
            view.GetComponents<IInitializable>()
                .Where(viewItem => viewItem.GetType() != typeof(T))
                .ToList()
                .ForEach(component => component.Initialize());

            view.GetComponentsInChildren<IInitializable>()
                .ToList()
                .ForEach(component => component.Initialize());
        }

        protected void SetupClosableView(AbstractView abstractView)
        {
            SetupView(abstractView);

            var closableView = abstractView.GetComponent<IClosableView>();
            var closableViewController = _closableViewControllerFactory.Create(closableView);
        }

        protected void SetupClosableView(AbstractView abstractView, ClosableViewType closableViewType)
        {
            SetupView(abstractView);

            var closableView = abstractView.GetComponent<IClosableView>();
            var closableViewController = _closableViewControllerFactory.Create(closableView);

            _closableViewRegistrar.RegisterClosableView(closableViewType, closableViewController);
        }
    }
}
