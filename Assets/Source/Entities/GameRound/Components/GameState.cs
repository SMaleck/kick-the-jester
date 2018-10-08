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
            _jesterEntity = jesterEntity;
            _userControlService = userControlService;
            _audioService = audioService;
            _particleService = particleService;
            
            _userControlService.OnPause
                .Subscribe(_ => model.IsPaused.Value = !model.IsPaused.Value)
                .AddTo(owner);

            _jesterEntity.OnKicked
                .Subscribe(_ => model.OnRoundStart.Execute())
                .AddTo(owner);

            _jesterEntity.OnLanded
                .Subscribe(_ => model.OnRoundEnd.Execute())
                .AddTo(owner);
        }


        private void OnPauseChanged(bool IsPaused)
        {
            // ToDo Pause Audio and ParticleService. NOTE: Unpause on start
        }
    }
}
