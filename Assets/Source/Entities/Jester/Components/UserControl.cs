using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Components
{
    public class UserControl : MonoBehaviour
    {
        private KickForceManager kickForceManager;

        void Start()
        {
            kickForceManager = gameObject.GetComponent<KickForceManager>();
        }

        void Update()
        {
            if (IsKickAction())
            {
                ApplyKick();
            }
        }


        private bool IsKickAction()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }


        // Applies the actual Kick to the Jester
        private void ApplyKick()
        {
            Vector3 AppliedForce = kickForceManager.GetAppliedKickForce();
            Singletons.jester.ApplyKick(AppliedForce);
        }
    }
}

