using Assets.Source.App;
using Assets.Source.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Components
{
    public class Movement : BaseComponent
    {
        private Rigidbody2D entityBody;
        private KickForceManager kickForceManager;


        // ------------------------ START
        private void Start()
        {
            kickForceManager = gameObject.GetComponent<KickForceManager>();
            entityBody = gameObject.GetComponent<Rigidbody2D>();

            // Register with controls
            Singletons.userControl.AttachForKick(KickForward);

            DeactivateOnStates(new List<GameStateMachine.GameState>() { GameStateMachine.GameState.Paused, GameStateMachine.GameState.End });
        }


        private void KickForward()
        {
            if (!IsActive) { return; }

            Vector3 AppliedForce = kickForceManager.GetAppliedKickForce();
            entityBody.AddForce(AppliedForce);
        }
    }
}
