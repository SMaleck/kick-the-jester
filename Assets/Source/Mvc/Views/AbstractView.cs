using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public abstract class AbstractView : MonoBehaviour, IInitializable
    {        
        protected virtual void Start()
        {
            Initialize();
        }

        public abstract void Initialize();        
    }
}
