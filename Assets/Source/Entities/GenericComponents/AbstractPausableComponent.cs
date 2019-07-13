using UniRx;

namespace Assets.Source.Entities.GenericComponents
{    
    public abstract class AbstractPausableComponent<T> : AbstractComponent<T> where T : AbstractPausableMonoEntity
    {        
        protected BoolReactiveProperty IsPaused = new BoolReactiveProperty();

        protected AbstractPausableComponent(T owner)
            : base(owner)
        {
            owner.IsPaused
                .Subscribe(value => IsPaused.Value = value)
                .AddTo(Disposer);
        }
    }
}
