﻿using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.Config;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class KickForce : AbstractComponent<JesterContainer>
    {
        private readonly JesterMovementConfig config;
        private readonly GameLogicContainer gameLogic;
        private readonly UserControl userControl;

        private bool isActive = false;       
        private bool isInitialKick = true;

        private Vector3 forceDirection = new Vector3(1, 1, 0);

        // Initial Kick on round start        
        private float maxKickForceFactor = 1f;
        private float kickForceFactor = 1f;
        private bool kickForceFactorGrows = true;
        
        // Shoots during flight        
        private int shootCount;


        public KickForce(JesterContainer owner, JesterMovementConfig config, GameLogicContainer gameLogic, UserControl userControl)
            : base(owner, true)
        {
            this.config = config;
            this.gameLogic = gameLogic;
            this.userControl = userControl;
            this.shootCount = config.ShootCount;

            owner.AvailableShotsProperty.Value = shootCount;

            // Listen to user Input
            userControl.OnKick(OnInputProxy);


            // Deactivate when Game Ends
            gameLogic.StateProperty
                     .Subscribe((GameState state) => { isActive = !state.Equals(GameState.Paused) && !state.Equals(GameState.End); })
                     .AddTo(owner);            
        }


        // On Pause deactivate
        protected override void OnPause(bool isPaused)
        {
            isActive = !isPaused;
        }


        // UPDATE
        protected override void Update()
        {
            if (isInitialKick)
            {
                UpdateInitialKickForceFactor();
                owner.RelativeKickForce = kickForceFactor.AsRelativeTo1(maxKickForceFactor);
            }            

            // Limit Velocity
            if (owner.goBody.velocity.magnitude > config.MaxVelocity)
            {
                owner.goBody.velocity = owner.goBody.velocity.normalized * config.MaxVelocity;
            }

            // Update Owner's relative velocity
            owner.RelativeVelocity = owner.goBody.velocity.magnitude.AsRelativeTo1(config.MaxVelocity);
        }


        private void UpdateInitialKickForceFactor()
        {
            float x = Time.deltaTime;

            if (kickForceFactor + x > maxKickForceFactor) { kickForceFactorGrows = false; }
            if (kickForceFactor - x < 0.01f) { kickForceFactorGrows = true; }

            kickForceFactor = kickForceFactorGrows ? 
                kickForceFactor + x 
                : kickForceFactor - x;
        }


        /* --------------------------------------------------------------------------- */
        #region INPUT HANDLING

        // Proxy to catch user input. Depending on the current state we kick or shoot
        private void OnInputProxy()
        {
            if(!isActive) { return; }

            if (isInitialKick)
            {
                OnInputKick();
                return;
            }

            OnInputShoot();
        }


        // Initial Kick at the round start
        private void OnInputKick()
        {
            if (!isInitialKick) { return; }
            
            isInitialKick = false;
            MessageBroker.Default.Publish(JesterEffects.Kick);

            Vector3 appliedForce = forceDirection * (config.KickForce * kickForceFactor);
            ApplyForce(appliedForce);
        }


        // In-flight force
        private void OnInputShoot()
        {
            if (shootCount <= 0) { return; }

            shootCount--;
            owner.AvailableShotsProperty.Value = shootCount;
            MessageBroker.Default.Publish(JesterEffects.Shot);            

            Vector3 appliedForce = forceDirection * config.ShootForce;
            ApplyForce(appliedForce);
        }


        private void ApplyForce(Vector3 appliedForce)
        {
            owner.goBody.AddForce(appliedForce, ForceMode2D.Impulse);
        }

        #endregion     
    }
}
