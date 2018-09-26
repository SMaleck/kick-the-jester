using UniRx;

namespace Assets.Source.Mvc.Models
{
    // ToDo Pause doesn't work as expected
    public class GameStateModel
    {
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty();
        public ReactiveCommand OnRoundStart = new ReactiveCommand();
        public ReactiveCommand OnRoundEnd = new ReactiveCommand();
    }
}
