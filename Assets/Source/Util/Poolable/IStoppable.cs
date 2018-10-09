namespace Assets.Source.Util.Poolable
{
    public interface IStoppable
    {
        bool IsPaused { get; }

        void Stop();
        void Pause();
        void Resume();
    }
}
