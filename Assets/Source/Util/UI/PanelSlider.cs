using Assets.Source.Services.Audio;
using DG.Tweening;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Util.UI
{
    public class PanelSlider : AbstractDisposable
    {
        private readonly PanelSliderConfig _config;

        private readonly RectTransform _owner;
        private readonly Vector3 _shownPosition;
        private readonly Vector3 _hiddenPosition;

        private float SlideSeconds => _config.slideInFrom.Equals(SlideDirection.Instant) ? 0 : _config.slideTimeSeconds;

        private readonly ReactiveCommand _onOpenCompleted;
        public IObservable<Unit> OnOpenCompleted => _onOpenCompleted;

        private readonly ReactiveCommand _onCloseCompleted;
        public IObservable<Unit> OnCloseCompleted => _onCloseCompleted;

        private readonly Tween _openTween;
        private readonly Tween _closeTween;

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

            _onOpenCompleted = new ReactiveCommand().AddTo(Disposer);
            _onCloseCompleted = new ReactiveCommand().AddTo(Disposer);

            var time = Math.Max(0, SlideSeconds);

            _openTween = CreateOpenSequence(
                _owner,
                _shownPosition,
                time);

            _closeTween = CreateCloseSequence(
                _owner,
                _hiddenPosition,
                time);
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

        private Tween CreateOpenSequence(
            RectTransform target,
            Vector3 targetVector,
            float durationSeconds)
        {
            var slideTween = CreateSlideTween(
                target,
                targetVector,
                durationSeconds,
                Ease.OutBounce);

            return DOTween.Sequence()
                .AppendCallback(() => { target.gameObject.SetActive(true); })
                .Append(slideTween)
                .AppendCallback(() => { _onOpenCompleted.Execute(); })
                .SetAutoKill(false)
                .Pause()
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
        }

        private Tween CreateCloseSequence(
            RectTransform target,
            Vector3 targetVector,
            float durationSeconds)
        {
            var slideTween = CreateSlideTween(
                target,
                targetVector,
                durationSeconds,
                Ease.InExpo);

            return DOTween.Sequence()
                .AppendCallback(() => { target.gameObject.SetActive(true); })
                .Append(slideTween)
                .AppendCallback(() =>
                {
                    _owner.gameObject.SetActive(false);
                    _onCloseCompleted.Execute();
                })
                .SetAutoKill(false)
                .Pause()
                .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
        }

        private Tween CreateSlideTween(
            RectTransform target,
            Vector3 targetVector,
            float durationSeconds,
            Ease easeType)
        {
            Tween slideTween;

            switch (_config.slideInFrom)
            {
                case SlideDirection.Top:
                case SlideDirection.Bottom:
                    slideTween = target
                        .DOLocalMoveY(targetVector.y, durationSeconds)
                        .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
                    break;

                case SlideDirection.Left:
                case SlideDirection.Right:
                    slideTween = _owner
                        .DOLocalMoveX(targetVector.y, durationSeconds)
                        .AddTo(Disposer, TweenDisposalBehaviour.Rewind);
                    break;

                default:
                    return null;
            }

            if (_config.useBounce)
            {
                slideTween.SetEase(easeType);
            }

            return slideTween;
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

            _openTween.Restart();
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

            _closeTween.Restart();
        }

        #endregion
    }
}
