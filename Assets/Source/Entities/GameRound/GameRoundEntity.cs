using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Source.Entities.GameRound
{
    public class GameRoundEntity : AbstractMonoEntity
    {
        public enum GameState { Launch, Flight, End, Paused }
        public ReactiveProperty<GameState> StateProperty = new ReactiveProperty<GameState>();

        public override void Initialize()
        {            
        }
    }
}
