using System;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.GameState
{    
    public class GameStateModel : AbstractDisposable
    {
        private readonly ReactiveProperty<bool> _isPaused;
        public IReadOnlyReactiveProperty<bool> IsPaused => _isPaused;

        private readonly Subject<Unit> _onRoundStart;
        public IObservable<Unit> OnRoundStart => _onRoundStart;

        private readonly Subject<Unit> _onRoundEnd;
        public IObservable<Unit> OnRoundEnd => _onRoundEnd;


        public GameStateModel()
        {
            _isPaused = new ReactiveProperty<bool>().AddTo(Disposer);
            _onRoundStart = new Subject<Unit>().AddTo(Disposer);
            _onRoundEnd = new Subject<Unit>().AddTo(Disposer);
        }

        public void SetIsPaused(bool isPaused)
        {
            _isPaused.Value = isPaused;
        }

        public void PublishRoundStart()
        {
            _onRoundStart.OnNext(Unit.Default);
        }

        public void PublishRoundEnd()
        {
            _onRoundEnd.OnNext(Unit.Default);
        }
    }
}
