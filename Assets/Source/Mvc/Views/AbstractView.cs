using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public abstract class AbstractView : MonoBehaviour, IInitializable
    {        
        // ToDo Figure out how to make this get called automatically
        public void Initialize()
        {
            this.gameObject.SetActive(true);
            Setup();
        }

        public abstract void Setup();
    }
}
