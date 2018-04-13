using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Entities.Components
{
    public class KickForceManager : BaseComponent
    {
        private bool IsInitialKick = true;
        private Vector3 ForceDirection = new Vector3(1, 1, 0);

        private float maxForceFactor = 2;
        private float InitialKickForceFactor = 1f;
        private int Kicks = 3;


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


        // Calculates the Force that will be applied to the Kick
        public Vector3 GetAppliedKickForce()
        {
            // Apply initial Kickforce modulation
            if (IsInitialKick)
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
