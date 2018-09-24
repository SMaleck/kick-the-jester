using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class GameStateModel
    {
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty();
        public ReactiveCommand OnRoundStart = new ReactiveCommand();
        public ReactiveCommand OnRoundEnd = new ReactiveCommand();
    }
}
