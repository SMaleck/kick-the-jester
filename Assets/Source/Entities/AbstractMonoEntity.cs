using Assets.Source.Util;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractMonoEntity : AbstractMonoBehaviour, IInitializable
    {
        // ToDo [ARCH] This is only for GameObjects serialized in the scene. Danger of running Initialize twice if used incorrectly
        // Some MonoBehaviours are not injected Zenject, so Initialize runs from Start()
        private void Start()
        {
            Initialize();
        }

        public abstract void Initialize();
    }
}
