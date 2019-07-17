using Assets.Source.Util;
using Zenject;

namespace Assets.Source.Entities
{
    public abstract class AbstractMonoEntity : AbstractMonoBehaviour, IInitializable
    {
        // ToDo Figure out how to run this automatically through Zenject
        private void Start()
        {
            Initialize();
        }

        public abstract void Initialize();
    }
}
