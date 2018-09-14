using Assets.Source.Util;

namespace Assets.Source.Mvc
{
    public class AbstractController : AbstractDisposable
    {
        public virtual void Open() { }
        public virtual void Close() { }
    }
}
