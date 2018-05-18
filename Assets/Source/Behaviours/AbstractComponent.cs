using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public abstract class AbstractComponent<T> where T : MonoBehaviour
    {
        private delegate void UpdateHandler();

        protected readonly T owner;
        protected readonly bool isPausable;        

        public AbstractComponent(T owner, bool isPausable = true)
        {
            this.owner = owner;
            this.isPausable = isPausable;

            App.Cache.GameStateMachine.IsPausedProperty.Subscribe((bool value) => OnPause(value)).AddTo(owner);

            Observable.EveryUpdate().Subscribe(_ => UpdateProxy(Update)).AddTo(owner);
            Observable.EveryFixedUpdate().Subscribe(_ => UpdateProxy(FixedUpdate)).AddTo(owner);
            Observable.EveryLateUpdate().Subscribe(_ => UpdateProxy(LateUpdate)).AddTo(owner);
        }

        
        private void UpdateProxy(UpdateHandler handler)
        {
            if (isPausable && App.Cache.GameStateMachine.IsPaused) { return; }
            handler();
        }
        
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        protected virtual void OnPause(bool isPaused) { }
    }
}
