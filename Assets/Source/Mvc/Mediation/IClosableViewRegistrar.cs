namespace Assets.Source.Mvc.Mediation
{
    public interface IClosableViewRegistrar
    {
        void RegisterClosableView(ClosableViewType closableViewType, IClosableViewController closableViewController);
    }
}
