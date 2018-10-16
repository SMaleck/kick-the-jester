using Assets.Source.App;
using System;
using UniRx;
using UnityEngine.SceneManagement;

namespace Assets.Source.Services
{
    public enum Scenes
    {
        Init,
        Title,
        Game
    }

    public enum TransitionState
    {
        None,
        Before,
        Loading,
        After
    }

    // ToDo [IMPORTANT] State is changing in awkward order, check    
    public class SceneTransitionService
    {
        public const float LOADING_GRACE_PERIOD_SECONDS = 0.5f;

        private readonly ReactiveProperty<Scenes> _currentScene;
        public ReadOnlyReactiveProperty<Scenes> CurrentScene { get; private set; }

        private readonly ReactiveProperty<TransitionState> _state;
        public ReadOnlyReactiveProperty<TransitionState> State { get; private set; }

        private readonly BoolReactiveProperty _isLoading;
        public ReadOnlyReactiveProperty<bool> IsLoading { get; private set; }


        public SceneTransitionService()
        {           
            _currentScene = new ReactiveProperty<Scenes>(Scenes.Init);
            CurrentScene = _currentScene.ToReadOnlyReactiveProperty();

            _state = new ReactiveProperty<TransitionState>(TransitionState.None);
            State = _state.ToReadOnlyReactiveProperty();

            _isLoading = new BoolReactiveProperty(false);
            IsLoading = _isLoading.ToReadOnlyReactiveProperty();

            SetupSubscriptions();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        // ToDo Dispose
        private void SetupSubscriptions()
        {
            State.Subscribe(state =>
            {
                _isLoading.Value = !state.Equals(TransitionState.None);
                Logger.Log($"[SceneTransitionService] Transition State changed to [{state}]");
            });

            CurrentScene.Subscribe(scene => Logger.Log($"[SceneTransitionService] Scene changed to [{scene}]"));
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            Scenes parsed;
            Enum.TryParse<Scenes>(scene.name, out parsed);
            _currentScene.Value = parsed;

            Logger.Log($"[SceneTransitionService] Entering AFTER Grace Period");
            _state.Value = TransitionState.After;

            // ToDo Dispose
            Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                .Subscribe(_ =>
                {
                    _state.Value = TransitionState.None;
                    Logger.Log($"[SceneTransitionService] Loading Done! Current Scene: {CurrentScene.Value}");
                });
        }
                

        private void PrepareLoad(Scenes toLoad)
        {
            Logger.Log($"[SceneTransitionService] Transition Request to {toLoad}");
            Logger.Log($"[SceneTransitionService] Entering BEFORE Grace Period");
            _state.Value = TransitionState.Before;

            // ToDo Dispose
            Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                .Subscribe(_ =>
                {                    
                    Load(toLoad);                    
                });            
        }


        private void Load(Scenes toLoad)
        {            
            Logger.Log("[SceneTransitionService] Loading Async...");
            _state.Value = TransitionState.Loading;

            SceneManager.LoadSceneAsync(toLoad.ToString());
        }


        public void ToTitle()
        {
            PrepareLoad(Scenes.Title);                    
        }


        public void ToGame()
        {
            PrepareLoad(Scenes.Game);
        }
    }
}
