using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.UI.Panels
{
    public class AbstractPanel : MonoBehaviour
    {
        protected enum FadeDirection { Top, Bottom, Left, Right };
        protected PanelLoader owner;
        protected Rect canvasRect;

        [SerializeField] protected bool startActive = true;
                
        [Header("Transition Effect")]
        [SerializeField] protected bool useSlideTransition = false;
        [SerializeField] protected float slideTimeSeconds = 0.6f;
        [SerializeField] protected FadeDirection slideInFrom  = FadeDirection.Top;
        [SerializeField] protected bool useBounce = false;

        [Header("Transition Sounds")]
        [SerializeField] private AudioClip sfxShow;
        [SerializeField] private AudioClip sfxHide;

        protected Vector3 fadeInTarget;        


        public virtual void Setup()
        {
            fadeInTarget = transform.localPosition;

            this.owner = GetComponentInParent<PanelLoader>();
            canvasRect = owner.ParentCanvas.GetComponent<RectTransform>().rect;

            this.gameObject.SetActive(true);

            
            // Slide out instantly if we start inactive
            if (!startActive)
            {
                SlideOut().setTime(0);
            }
        }


        public virtual void Show()
        {
            SlideIn();
            if(sfxShow != null)
            {
                Kernel.AudioService.PlaySFX(sfxShow);
            }
        }


        public virtual void Hide()
        {
            SlideOut();
            if (sfxHide != null)
            {
                Kernel.AudioService.PlaySFX(sfxHide);
            }
        }


        protected LTDescr SlideIn()
        {
            LTDescr tween = CreateFadeTween(fadeInTarget);

            if (useBounce)
            {
                tween.setEaseOutBounce();
            }

            return tween;
        }


        protected LTDescr SlideOut()
        {
            Vector3 target = GetSlideOutTarget();
            LTDescr tween = CreateFadeTween(target);

            if (useBounce)
            {
                tween.setEaseInExpo();
            }

            return tween;
        }


        private Vector3 GetSlideOutTarget()
        {
            switch (slideInFrom)
            {
                case FadeDirection.Top:
                    return new Vector3(fadeInTarget.x, canvasRect.height, fadeInTarget.z);

                case FadeDirection.Bottom:
                    return new Vector3(fadeInTarget.x, -canvasRect.height, fadeInTarget.z);

                case FadeDirection.Left:
                    return new Vector3(-canvasRect.width, fadeInTarget.y, fadeInTarget.z);

                case FadeDirection.Right:
                    return new Vector3(canvasRect.width, fadeInTarget.y, fadeInTarget.z);

                default:
                    return Vector3.zero;
            }
        }


        private LTDescr CreateFadeTween(Vector3 to)
        {
            float time = useSlideTransition ? slideTimeSeconds : 0;
            LTDescr tween = new LTDescr();

            switch (slideInFrom)
            {
                case FadeDirection.Top:
                case FadeDirection.Bottom:
                    return LeanTween.moveLocalY(this.gameObject, to.y, time);                    

                case FadeDirection.Left:
                case FadeDirection.Right:
                    return LeanTween.moveLocalX(this.gameObject, to.x, time);                    

                default:
                    return tween;
            }            
        }
    }
}
