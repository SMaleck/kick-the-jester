using Assets.Source.Entities.GenericComponents;
using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using Assets.Source.Services;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UniRx;

namespace Assets.Source.Entities.GameRound.Components
{
    public class GameState : AbstractComponent<GameRoundEntity>
    {
        private readonly GameStateModel _model;
        private readonly JesterEntity _jesterEntity;
        private readonly UserControlService _userControlService;
        private readonly AudioService _audioService;
        private readonly ParticleService _particleService;

        public GameState(
            GameRoundEntity owner, 
            GameStateModel model,
            JesterEntity jesterEntity,
            UserControlService userControlService,             
            AudioService audioService, 
            ParticleService particleService)
            : base(owner)
        {
            _model = model;
            _jesterEntity = jesterEntity;
            _userControlService = userControlService;
            _audioService = audioService;
            _particleService = particleService;

            _audioService.ResetPausedSlots();
            _particleService.ResetPausedSlots();

            _userControlService.OnPause
                .Subscribe(_ => model.IsPaused.Value = !model.IsPaused.Value)
                .AddTo(owner);

            _jesterEntity.OnKicked
                .Subscribe(_ => model.OnRoundStart.Execute())
                .AddTo(owner);

            _jesterEntity.OnLanded
                .Subscribe(_ => model.OnRoundEnd.Execute())
                .AddTo(owner);

            _model.IsPaused
                .Subscribe(OnPauseChanged)
                .AddTo(owner);
        }


        private void OnPauseChanged(bool IsPaused)
        {
            _audioService.PauseEffects(IsPaused);
            _particleService.PauseAll(IsPaused);
        }
    }
}
