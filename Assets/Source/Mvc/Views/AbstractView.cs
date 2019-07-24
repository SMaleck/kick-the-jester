using Assets.Source.Util;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public abstract class AbstractView : AbstractDisposableMonoBehaviour, IInitializable
    {
        public bool IsActive => gameObject.activeSelf;

        public void Initialize()
        {
            Setup();
        }

        public abstract void Setup();

        public void SetActive(bool isActive)
        {
            this.gameObject.SetActive(true);
        }
    }
}
