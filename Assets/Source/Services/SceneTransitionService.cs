using Assets.Source.App;
using Assets.Source.Util;
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

    public class SceneTransitionService : AbstractDisposable
    {
        public const float LOADING_GRACE_PERIOD_SECONDS = 0.5f;

        private readonly ReactiveProperty<Scenes> _currentScene;
        public IReadOnlyReactiveProperty<Scenes> CurrentScene => _currentScene;

        private readonly Subject<Unit> _onSceneLoadStarted;
        public IObservable<Unit> OnSceneLoadStarted => _onSceneLoadStarted;

        private readonly Subject<Unit> _onSceneInitComplete;
        public IObservable<Unit> OnSceneInitComplete => _onSceneInitComplete;

        public SceneTransitionService()
        {
            _currentScene = new ReactiveProperty<Scenes>(Scenes.Init).AddTo(Disposer);

            _onSceneLoadStarted = new Subject<Unit>().AddTo(Disposer);
            _onSceneInitComplete = new Subject<Unit>().AddTo(Disposer);

            SetupSubscriptions();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void PublishOnSceneLoadStarted()
        {
            _onSceneLoadStarted.OnNext(Unit.Default);
        }

        public void PublishOnSceneInitComplete()
        {
            _onSceneInitComplete.OnNext(Unit.Default);
        }

        private void SetupSubscriptions()
        {
            CurrentScene.Subscribe(scene =>
                {
                    Logger.Log($"Scene changed to [{scene}]");
                })
                .AddTo(Disposer);
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            Scenes parsed;
            Enum.TryParse<Scenes>(scene.name, out parsed);
            _currentScene.Value = parsed;

            // ToDo Dispose
            Logger.Log($"Entering AFTER Grace Period");
            Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                .Subscribe(_ =>
                {
                    Logger.Log($"Loading Done! Current Scene: {CurrentScene.Value}");
                })
                .AddTo(Disposer);
        }


        private void LoadDelayed(Scenes toLoad)
        {
            Logger.Log($"Transition Request to [{toLoad}]");

            PublishOnSceneLoadStarted();

            Logger.Log($"Entering BEFORE Grace Period");
            Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                .Subscribe(_ =>
                {
                    SceneManager.LoadSceneAsync(toLoad.ToString());
                })
                .AddTo(Disposer);
        }

        public void ToTitle()
        {
            LoadDelayed(Scenes.Title);
        }


        public void ToGame()
        {
            LoadDelayed(Scenes.Game);
        }
    }
}
