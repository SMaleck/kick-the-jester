using Assets.Source.Mvc.Models;
using UniRx;

namespace Assets.Source.Entities
{
    public abstract class AbstractPausableMonoEntity : AbstractMonoEntity
    {        
        public BoolReactiveProperty IsPaused = new BoolReactiveProperty(false);

        private void Inject(GameStateModel gameStateModel)
        {
            gameStateModel.IsPaused
                .Subscribe(isPaused => IsPaused.Value = isPaused)
                .AddTo(this);
        }
    }
}
