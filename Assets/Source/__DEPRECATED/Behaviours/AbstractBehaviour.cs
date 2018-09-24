using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractBehaviour : MonoBehaviour
    {
        private BoolReactiveProperty _isPausedProperty = new BoolReactiveProperty(false);

        public BoolReactiveProperty IsPausedProperty
        {
            get { return Kernel.AppState != null ? Kernel.AppState.IsPausedProperty : _isPausedProperty; }            
        }

        public Transform goTransform
        {
            get { return gameObject.transform; }
        }
    }
}
