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

        protected void SetupView(AbstractView abstractView)
        {
            abstractView.GetComponents<IInitializable>()
                .ToList()
                .ForEach(component => component.Initialize());

            abstractView.GetComponentsInChildren<IInitializable>()
                .ToList()
                .ForEach(component => component.Initialize());
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
