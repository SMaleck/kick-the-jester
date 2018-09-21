using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class MotionBoot : AbstractComponent<JesterContainer>
    {
        private bool isActive = false;
        private bool wasKicked = false;
        private bool updateForceFactor = true;

        private readonly MotionConfig config;
        private FloatReactiveProperty RelativeKickForce;

        // Kick Force Factor caclulations 
        private float maxKickForceFactor = 1f;
        private float kickForceFactor = 1f;
        private bool kickForceFactorGrows = true;


        public MotionBoot(JesterContainer owner, MotionConfig config, GameLogicContainer gameLogic, UserControl userControl)
            : base(owner)
        {
            this.config = config;
            this.RelativeKickForce = owner.RelativeKickForceProperty;

            // Listen to user input, so we cann stop updating KickForceFactor
            userControl.OnKick(() => { updateForceFactor = false; });

            // Listen to Initial Kick
            MessageBroker.Default.Receive<JesterEffects>()
                                 .Where(e => e.Equals(JesterEffects.Kick))
                                 .Subscribe(_ => Execute())
                                 .AddTo(owner);

            // Deactivate when Game Ends
            gameLogic.StateProperty
                     .Subscribe((GameState state) => { isActive = !state.Equals(GameState.Paused) && !state.Equals(GameState.End); })
                     .AddTo(owner);
        }


        // UPDATE
        protected override void Update()
        {
            if (updateForceFactor)
            {
                UpdateInitialKickForceFactor();
                owner.RelativeKickForce = kickForceFactor.AsRelativeTo1(maxKickForceFactor);
            }
        }


        // Recalculates the initial force factor
        private void UpdateInitialKickForceFactor()
        {
            float x = Time.deltaTime;

            if (kickForceFactor + x > maxKickForceFactor) { kickForceFactorGrows = false; }
            if (kickForceFactor - x < 0.01f) { kickForceFactorGrows = true; }

            kickForceFactor = kickForceFactorGrows ?
                kickForceFactor + x
                : kickForceFactor - x;
        }


        public void Execute()
        {
            if (!isActive || wasKicked) { return; }

            Vector3 appliedForce = config.Direction * (config.Force * RelativeKickForce.Value);

            owner.goBody.AddForce(appliedForce, ForceMode2D.Impulse);
            wasKicked = true;
        }
    }
}
