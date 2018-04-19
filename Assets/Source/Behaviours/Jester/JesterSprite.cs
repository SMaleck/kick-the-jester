using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class JesterSprite : AbstractBehaviour
    {
        private bool ListenForImpacts = false;
        private bool isRotating = false;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);

        private float RotationSpeed = 5f;
        private float minRotationSpeed = 1f;
        private float maxRotationSpeed = 100f;

        public void Awake()
        {
            App.Cache.jester.GetBehaviour<FlightRecorder>().OnStartedFlight(
                () => { ListenForImpacts = true; });

            App.Cache.jester.GetBehaviour<FlightRecorder>().OnLanded(
                () => { isRotating = false; });
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (ListenForImpacts)
            {
                SetRotation();
            }                        
        }


        public void Update()
        {
            if (isRotating)
            {
                gameObject.transform.Rotate(rotationDirection * RotationSpeed * Time.deltaTime);
            }
        }


        private void SetRotation()
        {
            isRotating = true;
            RotationSpeed = UnityEngine.Random.Range(minRotationSpeed, maxRotationSpeed);
        }
    }
}
