using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.Config;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class MotionShoot : AbstractComponent<JesterContainer>
    {
        private bool isActive = false;

        private readonly MotionShootConfig config;
        private IntReactiveProperty availableShots;


        public MotionShoot(JesterContainer owner, MotionShootConfig config, GameLogicContainer gameLogic, UserControl userControl)
            : base(owner)
        {
            this.config = config;
            this.availableShots = owner.AvailableShotsProperty;
            availableShots.Value = config.Count;

            // Listen to Initial Kick, when it happened, we start listening to input
            MessageBroker.Default.Receive<JesterEffects>()
                                 .Where(e => e.Equals(JesterEffects.Kick))
                                 .Subscribe(_ => userControl.OnKick(Execute))
                                 .AddTo(owner);

            // Deactivate when Game Ends
            gameLogic.StateProperty
                     .Subscribe((GameState state) => { isActive = !state.Equals(GameState.Paused) && !state.Equals(GameState.End); })
                     .AddTo(owner);
        }


        private void Execute()
        {
            if(!isActive || availableShots.Value <= 0) { return; }

            availableShots.Value--;            
            MessageBroker.Default.Publish(JesterEffects.Shot);

            Vector3 appliedForce = config.Direction * config.Force;

            owner.goBody.AddForce(appliedForce, ForceMode2D.Impulse);
        }
    }
}
