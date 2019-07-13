using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Entities.GenericComponents
{    
    public abstract class AbstractComponent<T> : AbstractDisposable where T : AbstractMonoEntity
    {
        protected readonly T Owner;        

        protected AbstractComponent(T owner)
        {
            this.Owner = owner;
            Disposer.AddTo(owner);
        }
    }
}
