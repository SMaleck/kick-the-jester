using Assets.Source.Behaviours.Jester;
using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Pickup : AbstractItem
    {
        [Range(1, 5000)]
        public int CurrencyAmount = 5;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Jester jester;
            if (!TryGetJester(collision, out jester))
            {
                return;
            }

            App.Cache.currencyManager.AddPickup(CurrencyAmount);

            // Disable this trigger
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
