using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Entities.GenericComponents;

namespace Assets.Source.Entities.GameRound.Components
{
    public class RoundStatsRecorder : AbstractComponent<GameRoundEntity>
    {
        public RoundStatsRecorder(GameRoundEntity owner) : base(owner)
        {
        }
    }
}
