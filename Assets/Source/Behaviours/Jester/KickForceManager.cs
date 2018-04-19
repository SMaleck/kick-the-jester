using Assets.Source.App;
using Assets.Source.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class KickForceManager : AbstractBehaviour
    {
        #region PROPERTIES

        private bool isInitialKick = true;
        private Vector3 forceDirection = new Vector3(1, 1, 0);

        private float maxForceFactor = 2;
        private float initialKickForceFactor = 1f;
        private int kicksAvailable = 1;

        private Rigidbody2D entityBody;

        #endregion

        #region UNITY LIFECYCLE

        // ------------------------ START
        private void Start()
        {
            entityBody = gameObject.GetComponent<Rigidbody2D>();

            // Listen for events
            App.Cache.userControl.AttachForKick(this.KickForward);
            App.Cache.playerProfile.OnProfileLoaded(this.OnPlayerProfileLoaded);

            // Prevent kicking during pause or after game is over
            DeactivateOnStates(new List<GameStateMachine.GameState>() { GameStateMachine.GameState.Paused, GameStateMachine.GameState.End });
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInitialKickForceFactor();            
        }

        private void UpdateInitialKickForceFactor()
        {
            float x = Time.deltaTime;

            initialKickForceFactor = (initialKickForceFactor + x) % maxForceFactor;
        }

        #endregion

        #region EVENT HANDLERS

        private void OnPlayerProfileLoaded(PlayerProfile profile)
        {
            kicksAvailable = profile.KickCount;
        }
        
        private void KickForward()
        {
            if (!IsActive || !CanKick()) { return; }

            TrackKickUsage();
            entityBody.AddForce(GetAppliedKickForce());

            if (isInitialKick) { isInitialKick = false; }
        }

        #endregion

        #region METHODS

        public int GetRelativeKickForce()
        {
            return MathUtil.AsPercent(initialKickForceFactor, maxForceFactor);
        }

        private bool CanKick()
        {
            return kicksAvailable > 0;
        }

        private void TrackKickUsage()
        {
            kicksAvailable--;
        }
        
        // Calculates the Force that will be applied to the Kick
        private Vector3 GetAppliedKickForce()
        {
            float currentForceMagnitude = isInitialKick ? App.Cache.playerProfile.KickForce * initialKickForceFactor
                : App.Cache.playerProfile.KickForce;
            
            return forceDirection * currentForceMagnitude;
        }
        
        #endregion
    }
}
