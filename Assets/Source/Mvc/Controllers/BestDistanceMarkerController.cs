using Assets.Source.Entities.Jester;
using Assets.Source.Features.Statistics;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class BestDistanceMarkerController : AbstractDisposable
    {
        private readonly BestDistanceMarkerView _view;
        private readonly JesterEntity _jesterEntity;

        public BestDistanceMarkerController(
            BestDistanceMarkerView view,
            IStatisticsModel statisticsModel,
            JesterEntity jesterEntity)
        {
            _view = view;

            _jesterEntity = jesterEntity;

            _view.JesterOrigin = _jesterEntity.Position;
            _view.UpdateBestDistanceInstant(statisticsModel.BestDistance.Value);

            statisticsModel.BestDistance
                .Subscribe(value => _view.UpdateBestDistance(value))
                .AddTo(Disposer);
        }
    }
}
