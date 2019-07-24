using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public abstract class AbstractView : MonoBehaviour, IInitializable
    {
        public Component Disposer => this;

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
