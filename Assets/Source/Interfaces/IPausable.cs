public interface IPausable
{
    bool IsPaused { get; set; }
    void SetPause(bool IsPaused);
}
