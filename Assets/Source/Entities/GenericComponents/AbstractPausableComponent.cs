using UniRx;

namespace Assets.Source.Entities.GenericComponents
{
    public abstract class AbstractPausableComponent<T> : AbstractComponent<T> where T : AbstractMonoEntity
    {
        private delegate void UpdateHandler();        

        public AbstractPausableComponent(T owner)
            : base(owner)
        {
            owner.IsPaused.Subscribe((bool value) => OnPause(value)).AddTo(owner);

            Observable.EveryUpdate().Subscribe(_ => UpdateProxy(Update)).AddTo(owner);
            Observable.EveryFixedUpdate().Subscribe(_ => UpdateProxy(FixedUpdate)).AddTo(owner);
            Observable.EveryLateUpdate().Subscribe(_ => UpdateProxy(LateUpdate)).AddTo(owner);
        }


        private void UpdateProxy(UpdateHandler handler)
        {
            if (owner.IsPaused.Value) { return; }
            handler();
        }

        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        protected abstract void OnPause(bool isPaused);
    }
}
