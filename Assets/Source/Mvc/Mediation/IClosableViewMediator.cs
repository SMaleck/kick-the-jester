namespace Assets.Source.Mvc.Mediation
{
    public interface IClosableViewMediator
    {
        void Open(ClosableViewType closableViewType);
        void Close(ClosableViewType closableViewType);
    }
}
