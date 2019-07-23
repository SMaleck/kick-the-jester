using Assets.Source.Services;
using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views.PartialViews
{
    public class PickupFeedbackView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, PickupFeedbackView> { }

        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Punch Scale Settings")]
        [SerializeField] private Vector3 _punchScale;
        [SerializeField] private float _punchScaleDuration;
        [SerializeField] private int _punchScaleVibration;

        [Header("Punch Position Settings")]
        [SerializeField] private Vector3 _punchPosition;
        [SerializeField] private float _punchPositionDuration;
        [SerializeField] private int _punchPositionVibration;

        [Header("Fading Settings")]
        [SerializeField] private float _fadeDelaySeconds;
        [SerializeField] private float _fadeSeconds;

        private Tween _pickupFeedbackTween;

        // ToDo This is not correct
        public bool IsPlaying => _pickupFeedbackTween.IsPlaying();

        public override void Setup()
        {
            _pickupFeedbackTween = CreateSequence();
        }

        private Sequence CreateSequence()
        {
            var punchScaleTween = _labelText.transform.DOPunchScale(
                _punchScale,
                _punchScaleDuration,
                _punchScaleVibration);

            var punchPositionTween = _labelText.transform.DOPunchPosition(
                _punchPosition,
                _punchPositionDuration,
                _punchPositionVibration);

            var fadeAwayTween = _canvasGroup.DOFade(0, _fadeSeconds);

            return DOTween.Sequence()
                .Join(punchScaleTween)
                .Join(punchPositionTween)
                .AppendInterval(_fadeDelaySeconds)
                .Append(fadeAwayTween)
                .SetAutoKill(false)
                .Pause();
        }

        public void SetCurrencyAmountWithAnimation(int amount)
        {
            _labelText.text = TextService.CurrencyAmount(amount);

            _pickupFeedbackTween.Restart();
            _pickupFeedbackTween.PlayForward();
        }
    }
}
