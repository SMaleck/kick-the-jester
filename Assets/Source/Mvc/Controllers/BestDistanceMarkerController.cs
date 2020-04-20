using Assets.Source.Features.Statistics;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using System.Linq;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class BestDistanceMarkerController : AbstractDisposable
    {
        public BestDistanceMarkerController(
            BestDistanceMarkerView bestDistanceMarkerView,
            IStatisticsModel statisticsModel)
        {
            bestDistanceMarkerView.SetBestDistanceInstant(
                statisticsModel.BestDistanceUnits.Value);

            statisticsModel.BestDistanceUnits
                .Skip(1)
                .Subscribe(bestDistanceMarkerView.SetBestDistance)
                .AddTo(Disposer);
        }
    }
}
