using Assets.Source.App;
using UnityEngine;

// ToDo: Should this be on te Jester prefab or separate?
namespace Assets.Source.Entities.Components
{
    public class KickForceManager : BaseComponent
    {
        private bool IsInitialKick = true;
        private float BaseForceMagnitude = 600f;
        private Vector3 ForceDirection = new Vector3(1, 1, 0);

        private float maxForceFactor = 2;
        private float InitialKickForceFactor = 1f;
        private int Kicks = 3;


        // Update is called once per frame
        void Update()
        {
            UpdateInitialKickForceFactor();
            Singletons.uiManager.kickForceUI.UpdateUI(MathUtil.AsPercent(InitialKickForceFactor, maxForceFactor));
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
            float currentForceMagnitude = BaseForceMagnitude;

            return ForceDirection * currentForceMagnitude;
        }


        private Vector3 GetInitialKickForce()
        {
            IsInitialKick = false;
            float currentForceMagnitude = BaseForceMagnitude * InitialKickForceFactor;

            return ForceDirection * currentForceMagnitude;
        }
    }
}
