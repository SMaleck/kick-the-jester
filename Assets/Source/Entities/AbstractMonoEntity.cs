using Assets.Source.Util;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractMonoEntity : AbstractMonoBehaviour, IInitializable
    {
        // ToDo Some MonoBehaviours are not injected Zenject, so Initialize runs from Start()
        private void Start()
        {
            Initialize();
        }

        public abstract void Initialize();
    }
}
