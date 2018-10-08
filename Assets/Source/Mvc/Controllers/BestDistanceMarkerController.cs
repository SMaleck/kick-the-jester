using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class BestDistanceMarkerController : AbstractController
    {
        private readonly BestDistanceMarkerView _view;
        private readonly ProfileModel _profileModel;

        public BestDistanceMarkerController(BestDistanceMarkerView view, ProfileModel profileModel)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _profileModel = profileModel;

            _profileModel.BestDistance
                .Subscribe(value => _view.BestDistance = value)
                .AddTo(Disposer);
        }
    }
}
