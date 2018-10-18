using Assets.Source.Mvc.ServiceControllers;
using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Util.UI
{   
    public class PanelSlider
    {
        private readonly PanelSliderConfig _config;

        private readonly RectTransform _owner;        
        private readonly Vector3 _shownPosition;
        private readonly Vector3 _hiddenPosition;        

        private float SlideSeconds => _config.slideInFrom.Equals(SlideDirection.Instant) ? 0 : _config.slideTimeSeconds;

        public ReactiveCommand OnOpenCompleted = new ReactiveCommand();
        public ReactiveCommand OnCloseCompleted = new ReactiveCommand();

        // ToDo Auto-Setup doesn't work correctly. HiddenPosition is still within bounds
        protected PanelSlider(RectTransform owner, RectTransform container, PanelSliderConfig config)            
        {
            _config = config;
            _owner = owner;            
            _shownPosition = owner.localPosition;
            _hiddenPosition = GetHiddenPosition(container);
        }

        public PanelSlider(RectTransform owner, PanelSliderConfig config, Vector3 shownPosition, Vector3 hiddenPosition)
        {
            _config = config;
            _owner = owner;            
            _shownPosition = shownPosition;
            _hiddenPosition = hiddenPosition;            
        }

        private Vector3 GetHiddenPosition(RectTransform container)
        {
            // HACK This only works because panels are fullscreen and is NOT general purpose
            // For some reason the values do not reflect the actual fullscreen width/height but are smaller
            var yDistance = container.rect.height * 2.2f;
            var xDistance = container.rect.width * 2.2f;

            switch (_config.slideInFrom)
            {
                case SlideDirection.Top:
                    var y = _owner.rect.height;
                    return new Vector3(_shownPosition.x, _shownPosition.y + yDistance, _shownPosition.z);

                case SlideDirection.Bottom:
                    return new Vector3(_shownPosition.x, _shownPosition.y - yDistance, _shownPosition.z);

                case SlideDirection.Left:
                    return new Vector3(_shownPosition.x - xDistance, _shownPosition.y, _shownPosition.z);

                case SlideDirection.Right:
                    return new Vector3(_shownPosition.x + xDistance, _shownPosition.y, _shownPosition.z);

                default:
                    return Vector3.zero;
            }
        }

        private void PlaySoundEffect(ViewAudioEvent soundEffectType)
        {
            if (!_config.SoundEffectsEnabled)
            {
                return;
            }

            if (_config.slideInFrom.Equals(SlideDirection.Instant) && !_config.SoundEffectsEnabledOnInstant)
            {
                return;
            }

            MessageBroker.Default.Publish(soundEffectType);
        }

        private Tweener CreateSlideTweener(Vector3 to, float slideSeconds, Ease easeType)
        {
            Tweener tweener;
            switch (_config.slideInFrom)
            {
                case SlideDirection.Top:
                case SlideDirection.Bottom:
                    tweener = _owner.DOLocalMoveY(to.y, slideSeconds);
                    break;

                case SlideDirection.Left:
                case SlideDirection.Right:
                    tweener = _owner.DOLocalMoveX(to.x, slideSeconds);
                    break;

                default:
                    return null;
            }

            if (_config.useBounce)
            {
                tweener.SetEase(easeType);
            }

            return tweener;
        }


        #region SLIDING INTERFACE

        public void SlideOpen()
        {
            SlideOpen(SlideSeconds);
            PlaySoundEffect(ViewAudioEvent.PanelSlideOpen);
        }

        public void SetOpen()
        {
            _owner.anchoredPosition = _shownPosition;
            _owner.gameObject.SetActive(true);
        }

        private void SlideOpen(float slideSeconds)
        {
            var time = Math.Max(0, slideSeconds);

            if (time <= 0)
            {
                SetOpen();
                return;
            }

            _owner.gameObject.SetActive(true);

            Tweener tween = CreateSlideTweener(_shownPosition, time, Ease.OutBounce);
            tween.OnComplete(() => { OnOpenCompleted.Execute(); });
        }


        public void SlideClosed()
        {
            SlideClosed(SlideSeconds);
            PlaySoundEffect(ViewAudioEvent.PanelSlideClose);
        }

        public void SetClosed()
        {
            _owner.anchoredPosition = _hiddenPosition;
            _owner.gameObject.SetActive(false);
        }

        private void SlideClosed(float slideSeconds)
        {
            var time = Math.Max(0, slideSeconds);

            if (time <= 0)
            {
                SetClosed();
                return;
            }

            Tweener tween = CreateSlideTweener(_hiddenPosition, time, Ease.InExpo);
            tween.OnComplete(() =>
            {
                _owner.gameObject.SetActive(false);
                OnCloseCompleted.Execute();
            });
        }

        #endregion
    }
}
