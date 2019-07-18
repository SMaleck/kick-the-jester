using Assets.Source.Entities.GenericComponents;
using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UniRx;

namespace Assets.Source.Entities.GameRound.Components
{
    public class GameState : AbstractComponent<GameRoundEntity>
    {
        private readonly GameStateModel _model;
        private readonly UserInputModel _userInputModel;
        private readonly JesterEntity _jesterEntity;
        private readonly AudioService _audioService;
        private readonly ParticleService _particleService;

        public GameState(
            GameRoundEntity owner,
            GameStateModel model,
            UserInputModel userInputModel,
            JesterEntity jesterEntity,
            AudioService audioService,
            ParticleService particleService)
            : base(owner)
        {
            _model = model;
            _userInputModel = userInputModel;
            _jesterEntity = jesterEntity;
            _audioService = audioService;
            _particleService = particleService;

            _audioService.ResetPausedSlots();
            _particleService.ResetAll();

            _userInputModel.OnPause
                .Subscribe(_ => model.SetIsPaused(!model.IsPaused.Value))
                .AddTo(Disposer);

            _jesterEntity.OnKicked
                .Subscribe(_ => model.PublishRoundStart())
                .AddTo(Disposer);

            _jesterEntity.OnLanded
                .Subscribe(_ => model.PublishRoundEnd())
                .AddTo(Disposer);

            _model.IsPaused
                .Subscribe(OnPauseChanged)
                .AddTo(Disposer);
        }


        private void OnPauseChanged(bool IsPaused)
        {
            _audioService.PauseEffects(IsPaused);
            _particleService.PauseAll(IsPaused);
        }
    }
}
