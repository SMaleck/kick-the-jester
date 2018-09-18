using System;
using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using Assets.Source.Services.Savegame;
using UniRx;
using SceneTransitionService = Assets.Source.Services.SceneTransitionService;

namespace Assets.Source.Mvc.Controllers
{
    public class TitleController : AbstractController
    {
        private readonly TitleView _view;
        private readonly TitleModel _model;
        
        private readonly SceneTransitionService _sceneTransitionService;        
        private readonly SavegameService _savegameService;

        private const float StartDelaySeconds = 1.5f;

        public TitleController(TitleView view, TitleModel model, SceneTransitionService sceneTransitionService, SavegameService savegameService)
        {
            _view = view;
            _view.Initialize();

            _model = model;            
            _sceneTransitionService = sceneTransitionService;            
            _savegameService = savegameService;                  

            _view.OnStartClicked = OnStartClicked;

            _view.OnSettingsClicked = () => { _model.OpenSettings.Execute(); };
            _view.OnCreditsClicked = () => { _model.OpenCredits.Execute(); };
            _view.OnTutorialClicked = () => { _model.OpenTutorial.Execute(); };
        }


        private void OnStartClicked()
        {
            //Kernel.AudioService.PlayBGM(bgmTransition);

            if (_model.IsFirstStart.Value)
            {
                _model.OpenTutorial.Execute();
                return;
            }            
            
            Observable
                .Timer(TimeSpan.FromSeconds(StartDelaySeconds))
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);
        }
    }
}