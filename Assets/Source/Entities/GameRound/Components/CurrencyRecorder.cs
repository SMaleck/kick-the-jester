using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Entities.GenericComponents;

namespace Assets.Source.Entities.GameRound.Components
{
    public class CurrencyRecorder : AbstractComponent<GameRoundEntity>
    {
        public CurrencyRecorder(GameRoundEntity owner) : base(owner)
        {
        }
    }
}
