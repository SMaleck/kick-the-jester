using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class TitleController : AbstractDisposable
    {
        private readonly TitleView _view;
        private readonly PlayerProfileModel _playerProfileModel;
        private readonly SceneTransitionService _sceneTransitionService;
        private readonly AudioService _audioService;
        private readonly IClosableViewMediator _closableViewMediator;

        private const float StartDelayFactor = 0.3f;

        public TitleController(
            TitleView view,
            PlayerProfileModel playerProfileModel,
            SceneTransitionService sceneTransitionService,
            AudioService audioService,
            IClosableViewMediator closableViewMediator)
        {
            _view = view;

            _playerProfileModel = playerProfileModel;
            _sceneTransitionService = sceneTransitionService;
            _audioService = audioService;
            _closableViewMediator = closableViewMediator;

            _view.OnStartClicked
                .Subscribe(_ => OnStartClicked())
                .AddTo(Disposer);

            _view.OnSettingsClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Settings))
                .AddTo(Disposer);

            _view.OnCreditsClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Credits))
                .AddTo(Disposer);

            _view.OnTutorialClicked
                .Subscribe(_ => _closableViewMediator.Open(ClosableViewType.Tutorial))
                .AddTo(Disposer);
        }


        private void OnStartClicked()
        {
            _audioService.PlayMusic(AudioClipType.Music_Transition, false);

            if (!_playerProfileModel.HasCompletedTutorial.Value)
            {
                _closableViewMediator.Open(ClosableViewType.Tutorial);
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