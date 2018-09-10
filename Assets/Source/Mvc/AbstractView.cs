using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc
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
