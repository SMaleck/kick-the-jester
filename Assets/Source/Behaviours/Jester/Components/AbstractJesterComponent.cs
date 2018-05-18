using UniRx;

namespace Assets.Source.Behaviours.Jester.Components
{
    public abstract class AbstractJesterComponent
    {
        private delegate void UpdateHandler();

        protected readonly Jester owner;
        protected readonly bool isPausable;        

        public AbstractJesterComponent(Jester owner, bool isPausable)
        {
            this.owner = owner;
            this.isPausable = isPausable;

            App.Cache.RepoRx.GameStateRepository.IsPausedProperty.Subscribe((bool value) => OnPause(value)).AddTo(owner);

            Observable.EveryUpdate().Subscribe(_ => UpdateProxy(Update)).AddTo(owner);
            Observable.EveryFixedUpdate().Subscribe(_ => UpdateProxy(FixedUpdate)).AddTo(owner);
            Observable.EveryLateUpdate().Subscribe(_ => UpdateProxy(LateUpdate)).AddTo(owner);
        }

        
        private void UpdateProxy(UpdateHandler handler)
        {
            if (isPausable && App.Cache.RepoRx.GameStateRepository.IsPaused) { return; }
            handler();
        }
        
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        protected virtual void OnPause(bool isPaused) { }
    }
}
