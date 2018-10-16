using Assets.Source.Mvc.Models.ViewModels;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using System;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TitleController : AbstractController
    {
        private readonly TitleView _view;
        private readonly TitleModel _model;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;

        private const float StartDelayFactor = 0.4f;

        public TitleController(TitleView view, TitleModel model, SceneTransitionService sceneTransitionService, AudioService audioService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _model = model;
            _audioService = audioService;
            _sceneTransitionService = sceneTransitionService;

            _view.OnStartClicked
                .Subscribe(_ => OnStartClicked())
                .AddTo(Disposer);

            _view.OnSettingsClicked
                .Subscribe(_ => _model.OpenSettings.Execute())
                .AddTo(Disposer);

            _view.OnCreditsClicked
                .Subscribe(_ => _model.OpenCredits.Execute())
                .AddTo(Disposer);

            _view.OnTutorialClicked
                .Subscribe(_ => _model.OpenTutorial.Execute())
                .AddTo(Disposer);
        }


        private void OnStartClicked()
        {
            _audioService.PlayMusic(_view._TransitionMusic, false);
            
            if (_model.IsFirstStart.Value)
            {
                _model.OpenTutorial.Execute();
                return;
            }


            var startDelaySeconds = _view._TransitionMusic.length * StartDelayFactor;

            Observable
                .Timer(TimeSpan.FromSeconds(startDelaySeconds))
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);
        }
    }
}