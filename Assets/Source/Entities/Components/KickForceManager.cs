using Assets.Source.App;
using Assets.Source.GameLogic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Components
{
    public class KickForceManager : BaseComponent
    {
        private bool IsInitialKick = true;
        private Vector3 ForceDirection = new Vector3(1, 1, 0);

        private float maxForceFactor = 2;
        private float InitialKickForceFactor = 1f;
        private int Kicks = 1;

        private Rigidbody2D entityBody;

        // ------------------------ START
        private void Start()
        {
            entityBody = gameObject.GetComponent<Rigidbody2D>();
            
            // Listen for events
            Singletons.userControl.AttachForKick(KickForward);
            Singletons.playerProfile.AddEventHandler(OnPlayerProfileLoaded);

            // Prevent kicking during pause or after game is over
            DeactivateOnStates(new List<GameStateMachine.GameState>() { GameStateMachine.GameState.Paused, GameStateMachine.GameState.End });
        }

        private void OnPlayerProfileLoaded(PlayerProfile profile)
        {
            Kicks = profile.KickCount;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInitialKickForceFactor();            
        }

        private void UpdateInitialKickForceFactor()
        {
            float x = Time.deltaTime;

            InitialKickForceFactor = (InitialKickForceFactor + x) % maxForceFactor;
        }

        private void KickForward()
        {
            if (!IsActive) { return; }

            Vector3 AppliedForce = GetAppliedKickForce();
            entityBody.AddForce(AppliedForce);
        }

        // Calculates the Force that will be applied to the Kick
        public Vector3 GetAppliedKickForce()
        {
            return GetAppliedKickForce(IsInitialKick);
        }

        public Vector3 GetAppliedKickForce(bool isInitialKick)
        {
            // Apply initial Kickforce modulation
            if (isInitialKick)
            {
                return GetInitialKickForce();
            }

            // Return Zero, if there are no Kicks left
            if (Kicks <= 0)
            {
                return Vector3.zero;
            }

            // Reduce amount of kicks left
            Kicks--;
            float currentForceMagnitude = Singletons.playerProfile.KickForce;

            return ForceDirection * currentForceMagnitude;
        }


        private Vector3 GetInitialKickForce()
        {
            IsInitialKick = false;
            float currentForceMagnitude = Singletons.playerProfile.KickForce * InitialKickForceFactor;

            return ForceDirection * currentForceMagnitude;
        }


        public int GetRelativeKickForce()
        {
            return MathUtil.AsPercent(InitialKickForceFactor, maxForceFactor);
        }
    }
}
