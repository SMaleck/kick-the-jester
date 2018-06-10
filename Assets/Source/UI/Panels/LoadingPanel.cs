using Assets.Source.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class LoadingPanel : MonoBehaviour
    {
        [Header("Panel Properties")]        
        [SerializeField] private Image spinnerImage;

        private CanvasGroup _canvasGroup;
        private CanvasGroup canvasGroup
        {
            get
            {
                if(_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }


        private float fadeTimeSeconds = 0.5f;

        private bool isRotating = false;
        private Vector3 rotationDirection = new Vector3(0, 0, -1);
        private float rotationSpeed = 5f;


        private void Start()
        {
            DontDestroyOnLoad(this);
            gameObject.SetActive(true);
        }


        private void Update()
        {
            if (isRotating)
            {
                spinnerImage.gameObject.transform.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
            }       
        }


        public void FadeIn(NotifyEventHandler callback = null)
        {
            LeanTween.alphaCanvas(canvasGroup, 0, fadeTimeSeconds)
                     .setEaseInCubic()
                     .setOnComplete(_ =>
                     {
                         gameObject.SetActive(false);
                         isRotating = false;

                         if (callback != null)
                         {
                             callback();
                         }
                     });
        }


        public void FadeOut(NotifyEventHandler callback = null)
        {
            isRotating = true;
            gameObject.SetActive(true);

            LeanTween.alphaCanvas(canvasGroup, 1, fadeTimeSeconds)
                     .setEaseInCubic()
                     .setOnComplete(_ =>
                     {
                         if (callback != null)
                         {
                             callback();
                         }
                     });
        }
    }
}
