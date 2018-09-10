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


    public class SceneTransitionService
    {
        public const float LOADING_GRACE_PERIOD_SECONDS = 0.5f;

        public Scenes CurrentScene { get; private set; }
        public ReactiveProperty<TransitionState> State { get; private set; }

        public BoolReactiveProperty IsLoading =  new BoolReactiveProperty(false);  


        public SceneTransitionService()
        {            
            CurrentScene = Scenes.Init;
            State = new ReactiveProperty<TransitionState>(TransitionState.None);

            // ToDo Dispose
            State.Subscribe(state => { IsLoading.Value = !state.Equals(TransitionState.None); });

            SceneManager.sceneLoaded += (Scene s, LoadSceneMode lm) =>
            {
                Scenes parsed;
                Enum.TryParse<Scenes>(s.name, out parsed);
                CurrentScene = parsed;

                State.Value = TransitionState.After;

                // ToDo Dispose
                Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                    .Subscribe(_ => 
                    {
                        State.Value = TransitionState.None;
                        Logger.Log("Loading Done! Current Scene: {0}", CurrentScene);                        
                    });
            };
        }
        

        private void PrepareLoad(Scenes toLoad)
        {
            Logger.Log("Scene Transition Request to {0}", toLoad);
            State.Value = TransitionState.Before;

            // ToDo Dispose
            Observable.Timer(TimeSpan.FromSeconds(LOADING_GRACE_PERIOD_SECONDS))
                .Subscribe(_ =>
                {                    
                    Load(toLoad);                    
                });            
        }


        private void Load(Scenes toLoad)
        {            
            Logger.Log("Loading Async...");
            State.Value = TransitionState.Loading;

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
