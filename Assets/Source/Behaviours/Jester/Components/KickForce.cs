using Assets.Source.App;
using Assets.Source.AppKernel;
using Assets.Source.GameLogic;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester.Components
{
    public class KickForce : AbstractComponent<Jester>
    {
        private bool isActive = false;

        private bool isInitialKick = true;
        private Vector3 forceDirection = new Vector3(1, 1, 0);

        private float maxForceFactor = 2;
        private float initialKickForceFactor = 1f;
        private bool initialFactorGrows = true;
        private int kickCount = 1;


        public KickForce(Jester owner, int kickCount)
            : base(owner, true)
        {
            this.kickCount = kickCount;
            App.Cache.userControl.OnKick(KickForward);

            // Prevent kicking during pause or after game is over
            App.Cache.GameStateMachine.StateProperty.Subscribe((GameState state) => 
            {
                isActive = !state.Equals(GameState.Paused) || !state.Equals(GameState.End);
            }).AddTo(owner);            
        }


        // UPDATE
        protected override void Update()
        {
            UpdateInitialKickForceFactor();            

            owner.RelativeKickForce = initialKickForceFactor.AsPercent(maxForceFactor);
        }


        private void UpdateInitialKickForceFactor()
        {
            float x = Time.deltaTime;

            if (initialKickForceFactor + x > maxForceFactor) { initialFactorGrows = false; }
            if (initialKickForceFactor - x < 0) { initialFactorGrows = true; }

            initialKickForceFactor = initialFactorGrows ? 
                initialKickForceFactor + x 
                : initialKickForceFactor - x;
        }


        // Kicks the Jester forward
        private void KickForward()
        {
            if (!isActive || kickCount <= 0) { return; }

            kickCount--;
            owner.goBody.AddForce(GetAppliedKickForce());

            if (isInitialKick) { isInitialKick = false; }
        }


        // Calculates the Force that will be applied to the Kick
        private Vector3 GetAppliedKickForce()
        {
            float currentForceMagnitude = isInitialKick ? Kernel.PlayerProfileService.KickForce * initialKickForceFactor
                : Kernel.PlayerProfileService.KickForce;
            
            return forceDirection * currentForceMagnitude;
        }       
    }
}
