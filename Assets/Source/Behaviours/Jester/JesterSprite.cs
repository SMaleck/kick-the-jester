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

        private void Awake()
        {
            App.Cache.RepoRx.JesterStateRepository.OnStartedFlight(
                () => { ListenForImpacts = true; });

            App.Cache.RepoRx.JesterStateRepository.OnLanded(
                () => { isRotating = false; });

            GetComponent<CollisionListener>().OnGround(SetRotation);
        }


        private void Update()
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
