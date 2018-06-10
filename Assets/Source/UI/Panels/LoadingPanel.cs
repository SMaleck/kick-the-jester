using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class LoadingPanel : MonoBehaviour
    {
        [Header("Panel Properties")]
        [SerializeField] private GameObject Panel;
        [SerializeField] private Image spinnerImage;

        private bool isRotating = false;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);
        private float rotationSpeed = 5f;


        private void Start()
        {            
            Panel.SetActive(false);
            Kernel.SceneTransitionService.IsLoadingProperty
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
            Panel.SetActive(true);
            isRotating = true;
        }
    }
}
