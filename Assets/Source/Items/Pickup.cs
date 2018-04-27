using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Items
{
    public class Pickup : AbstractItem
    {
        [Range(1, 5000)]
        public int CurrencyAmount = 5;

        protected override void Execute(Jester jester)
        {
            App.Cache.currencyManager.AddPickup(CurrencyAmount);
        }
    }
}
