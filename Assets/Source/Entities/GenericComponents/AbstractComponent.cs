namespace Assets.Source.Entities.GenericComponents
{
    // ToDo Add to owners disposer
    public abstract class AbstractComponent<T> where T : AbstractMonoEntity
    {        
        protected readonly T Owner;        

        public AbstractComponent(T owner)
        {
            this.Owner = owner;            
        }
    }
}
