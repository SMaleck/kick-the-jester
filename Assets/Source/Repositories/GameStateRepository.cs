using Assets.Source.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;

namespace Assets.Source.Repositories
{
    public enum GameState { Launch, Flight, End, Switching, Paused }

    public class GameStateRepository
    {       
        public ReactiveProperty<GameState> StateProperty = new ReactiveProperty<GameState>();
        public GameState State
        {
            get { return StateProperty.Value; }
            set { StateProperty.Value = value; }
        }
    }
}
