using Assets.Source.Mvc.Models;
using UniRx;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractPausableMonoEntity : AbstractMonoEntity
    {        
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty(false);

        [Inject]
        private void Inject(GameStateModel gameStateModel)
        {
            gameStateModel.IsPaused
                .Subscribe(isPaused => IsPaused.Value = isPaused)
                .AddTo(this);
        }
    }
}
