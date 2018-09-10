namespace Assets.Source.Util
{
    public interface IFactory<out T> where T : class
    {
        T CreateResource();
    }
}
