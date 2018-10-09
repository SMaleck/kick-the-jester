using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class BestDistanceMarkerController : AbstractController
    {
        private readonly BestDistanceMarkerView _view;
        private readonly ProfileModel _profileModel;
        private readonly JesterEntity _jesterEntity;

        public BestDistanceMarkerController(BestDistanceMarkerView view, ProfileModel profileModel, JesterEntity jesterEntity)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _profileModel = profileModel;
            _jesterEntity = jesterEntity;

            _view.JesterOrigin = _jesterEntity.Position;
            _view.UpdateBestDistanceInstant(_profileModel.BestDistance.Value);

            _profileModel.BestDistance
                .Subscribe(value => _view.UpdateBestDistance(value))
                .AddTo(Disposer);            
        }
    }
}
