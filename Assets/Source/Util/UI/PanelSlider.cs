using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.Util.UI
{
    public class PanelSlider
    {
        private readonly PanelSliderConfig _config;

        private readonly RectTransform _owner;
        private readonly Rect _container;
        private readonly Vector3 _hiddenPosition;
        private Vector3 _shownPosition;        

        private float SlideSeconds => _config.useSlideTransition ? _config.slideTimeSeconds : 0;


        public PanelSlider(RectTransform owner, Rect container, PanelSliderConfig config)
            : this(owner, container, config, owner.localPosition)
        { }

        public PanelSlider(RectTransform owner, Rect container, PanelSliderConfig config, Vector3 shownPosition)
        {
            _config = config;
            _owner = owner;
            _container = container;
            _shownPosition = shownPosition;

            _hiddenPosition = GetHiddenPosition();            
        }
        
        private Vector3 GetHiddenPosition()
        {
            switch (_config.slideInFrom)
            {
                case SlideDirection.Top:
                    return new Vector3(_shownPosition.x, _container.height, _shownPosition.z);

                case SlideDirection.Bottom:
                    return new Vector3(_shownPosition.x, -_container.height, _shownPosition.z);

                case SlideDirection.Left:
                    return new Vector3(-_container.width, _shownPosition.y, _shownPosition.z);

                case SlideDirection.Right:
                    return new Vector3(_container.width, _shownPosition.y, _shownPosition.z);

                default:
                    return Vector3.zero;
            }            
        }


        public void SlideIn()
        {
            SlideIn(SlideSeconds);
        }

        public void SlideIn(float slideSeconds)
        {
            _owner.gameObject.SetActive(true);

            var time = Math.Max(0, slideSeconds);

            Tweener tween = CreateSlideTweener(_shownPosition, time);

            if (_config.useBounce)
            {
                tween.SetEase(Ease.OutBounce);                
            }            
        }


        public void SlideOut()
        {            
            SlideOut(SlideSeconds);
        }

        public void SlideOut(float slideSeconds)
        {
            var time = Math.Max(0, slideSeconds);

            Tweener tween = CreateSlideTweener(_hiddenPosition, time);
            tween.OnComplete(() => { _owner.gameObject.SetActive(false); });

            if (_config.useBounce)
            {                
                tween.SetEase(Ease.InExpo);
            }            
        }


        private Tweener CreateSlideTweener(Vector3 to, float slideSeconds)
        {            
            switch (_config.slideInFrom)
            {
                case SlideDirection.Top:
                case SlideDirection.Bottom:
                    return _owner.DOLocalMoveY(_shownPosition.y, slideSeconds);

                case SlideDirection.Left:
                case SlideDirection.Right:
                    return _owner.DOLocalMoveX(_shownPosition.x, slideSeconds);

                default:
                    return null;
            }
        }
    }
}
