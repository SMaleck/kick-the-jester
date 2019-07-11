using Assets.Source.Entities.Jester;
using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Views;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class BestDistanceMarkerController : AbstractController
    {
        private readonly BestDistanceMarkerView _view;
        private readonly JesterEntity _jesterEntity;

        public BestDistanceMarkerController(
            BestDistanceMarkerView view,
            PlayerProfileModel playerProfileModel,
            JesterEntity jesterEntity)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _jesterEntity = jesterEntity;

            _view.JesterOrigin = _jesterEntity.Position;
            _view.UpdateBestDistanceInstant(playerProfileModel.BestDistance.Value);

            playerProfileModel.BestDistance
                .Subscribe(value => _view.UpdateBestDistance(value))
                .AddTo(Disposer);
        }
    }
}
