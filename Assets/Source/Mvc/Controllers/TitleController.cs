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
        private readonly OpenPanelModel _openPanelModel;
        private readonly TitleModel _model;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;

        private const float StartDelayFactor = 0.3f;

        public TitleController(
            TitleView view,
            OpenPanelModel openPanelModel,
            TitleModel model,
            SceneTransitionService sceneTransitionService,
            AudioService audioService)
            : base(view)
        {
            _view = view;
            _openPanelModel = openPanelModel;
            _view.Initialize();

            _model = model;
            _audioService = audioService;
            _sceneTransitionService = sceneTransitionService;

            _view.OnStartClicked
                .Subscribe(_ => OnStartClicked())
                .AddTo(Disposer);

            _view.OnSettingsClicked
                .Subscribe(_ => openPanelModel.OpenSettings())
                .AddTo(Disposer);

            _view.OnCreditsClicked
                .Subscribe(_ => openPanelModel.OpenCredits())
                .AddTo(Disposer);

            _view.OnTutorialClicked
                .Subscribe(_ => openPanelModel.OpenTutorial())
                .AddTo(Disposer);
        }


        private void OnStartClicked()
        {
            _audioService.PlayMusic(AudioClipType.Music_Transition, false);

            if (_model.IsFirstStart.Value)
            {
                _openPanelModel.OpenTutorial();
                return;
            }


            var startDelaySeconds = _view.TransitionMusic.length * StartDelayFactor;

            Observable
                .Timer(TimeSpan.FromSeconds(startDelaySeconds))
                .Subscribe(_ => _sceneTransitionService.ToGame())
                .AddTo(Disposer);
        }
    }
}