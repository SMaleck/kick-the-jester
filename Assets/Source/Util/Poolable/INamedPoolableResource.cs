namespace Assets.Source.Util.Poolable
{
    public interface INamedPoolableResource : IPoolableResource
    {
        string Name { get; }
    }
}
