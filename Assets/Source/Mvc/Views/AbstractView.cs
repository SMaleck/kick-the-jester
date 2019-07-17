using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    // ToDo Should this inherit from AbstractMonoBehaviour instead?
    public abstract class AbstractView : MonoBehaviour, IInitializable
    {
        public Component Disposer => this;

        public bool IsActive => gameObject.activeSelf;

        // ToDo Figure out how to make this get called automatically
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
