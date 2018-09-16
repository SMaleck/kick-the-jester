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


        // ToDo Figure out how to make this get called automatically
        public abstract void Initialize();        
    }
}
