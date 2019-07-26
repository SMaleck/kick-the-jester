using Assets.Source.Features.PlayerData;
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
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;

        private const float StartDelayFactor = 0.3f;

        public TitleController(
            TitleView view,
            OpenPanelModel openPanelModel,
            PlayerProfileModel playerProfileModel,
            SceneTransitionService sceneTransitionService,
            AudioService audioService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _openPanelModel = openPanelModel;
            _playerProfileModel = playerProfileModel;
            _sceneTransitionService = sceneTransitionService;
            _audioService = audioService;

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

            if (!_playerProfileModel.HasCompletedTutorial.Value)
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