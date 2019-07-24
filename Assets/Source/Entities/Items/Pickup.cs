using Assets.Source.Entities.Jester;
using Assets.Source.Features.PlayerData;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.Items
{
    public class Pickup : AbstractItemEntity
    {
        [Range(1, 5000)]
        [SerializeField] public int CurrencyAmount;

        private FlightStatsController _flightStatsController;

        [Inject]
        private void Inject(FlightStatsController flightStatsController)
        {
            _flightStatsController = flightStatsController;
        }

        protected override void Setup() { }

        protected override void Execute(JesterEntity jester)
        {
            _flightStatsController.AddCurrencyPickup(CurrencyAmount);
        }
    }
}
