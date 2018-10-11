using Assets.Source.Entities.GameRound.Components;
using Assets.Source.Entities.Jester;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.Items
{
    public class Pickup : AbstractItemEntity
    {
        [Range(1, 5000)]
        public int CurrencyAmount = 5;

        public override void Initialize() { }

        protected override void Execute(JesterEntity jester)
        {
            // ToDo Find a better solution for Currency pickup
            MessageBroker.Default.Publish(new CurrencyGainEvent() {Amount = CurrencyAmount});            
        }
    }
}
