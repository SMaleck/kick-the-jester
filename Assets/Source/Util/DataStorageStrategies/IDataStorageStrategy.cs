namespace Assets.Source.Util.DataStorageStrategies
{
    public interface IDataStorageStrategy
    {
        T Load<T>(string filename) where T : class;
        void Save<T>(string filename, T data);
    }
}
