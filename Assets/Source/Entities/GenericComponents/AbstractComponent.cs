using UniRx;

namespace Assets.Source.Entities.GenericComponents
{
    // ToDo Add to owners disposer
    public abstract class AbstractComponent<T> where T : AbstractMonoEntity
    {        
        protected readonly T owner;        

        public AbstractComponent(T owner)
        {
            this.owner = owner;            
        }
    }
}
