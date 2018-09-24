using Assets.Source.Entities.GenericComponents;
using Assets.Source.Entities.Jester;
using Assets.Source.Mvc.Models;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Entities.GameRound.Components
{
    public class GameState : AbstractComponent<GameRoundEntity>
    {
        private readonly UserControlService _userControlService;
        private readonly JesterEntity _jesterEntity;

        public GameState(GameRoundEntity owner, GameStateModel model, UserControlService userControlService, JesterEntity jesterEntity)
            : base(owner)
        {
            _userControlService = userControlService;
            _jesterEntity = jesterEntity;

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
    }
}
