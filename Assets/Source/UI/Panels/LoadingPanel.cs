using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private Image spinnerImage;

        private bool isRotating = false;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);
        private float rotationSpeed = 5f;


        private void Start()
        {
            gameObject.SetActive(false);
            App.Cache.Services.SceneTransitionService.IsLoadingProperty
                                                     .Where(e => e)
                                                     .Subscribe(_ => Activate())
                                                     .AddTo(this);
        }


        private void Update()
        {
            if (isRotating)
            {
                spinnerImage.gameObject.transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
            }            
        }


        private void Activate()
        {
            Debug.Log("HERE");
            gameObject.SetActive(true);
            isRotating = true;
        }
    }
}
