using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Behaviours.Items
{
    public class Pickup : AbstractItem
    {
        [Range(1, 5000)]
        public int CurrencyAmount = 5;

        protected override void Execute(JesterContainer jester)
        {
            App.Cache.GameLogic.currencyRecorder.AddPickup(CurrencyAmount);
        }
    }
}
