using Assets.Source.App;
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
        }


        public void KickForward()
        {            
            Vector3 AppliedForce = kickForceManager.GetAppliedKickForce();
            entityBody.AddForce(AppliedForce);
        }
    }
}
