using Assets.Source.Util;

namespace Assets.Source.Features.PlayerData
{
    public class FlightStatsController : AbstractDisposable
    {
        private readonly FlightStatsModel _flightStatsModel;

        public FlightStatsController(
            FlightStatsModel flightStatsModel)
        {
            _flightStatsModel = flightStatsModel;
        }

        public void AddCurrencyPickup(int amount)
        {
            if (amount <= 0) { return; }

            _flightStatsModel.Gains.Add(amount);
            _flightStatsModel.Collected.Value += amount;
        }
    }
}
