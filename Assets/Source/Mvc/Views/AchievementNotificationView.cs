using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.Data;
using Assets.Source.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using UniRx;

namespace Assets.Source.Mvc.Views
{
    public class AchievementNotificationView : AbstractView
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Animation")]
        [SerializeField] private RectTransform _containerParent;
        [SerializeField] private float _slideDurationSeconds;
        [SerializeField] private float _holdDelaySeconds;
        [SerializeField] private float _startY;
        [SerializeField] private float _targetY;

        private AchievementsIconConfig _achievementsIconConfig;
        private Tween _notificationTween;

        [Inject]
        private void Inject(AchievementsIconConfig achievementsIconConfig)
        {
            _achievementsIconConfig = achievementsIconConfig;
        }

        public override void Setup()
        {
            _titleText.text = TextService.AchievementUnlocked();
            _containerParent.anchoredPosition = new Vector2(_containerParent.anchoredPosition.x, _startY);

            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Input.GetKeyDown("q"))
                    {
                        ShowFor(AchievementId.BestDistance1);
                    }
                });
        }

        public void ShowFor(AchievementId achievementId)
        {
            if (_notificationTween != null && _notificationTween.IsPlaying())
            {
                _notificationTween.Rewind();
                _notificationTween.Kill();
            }

            _iconImage.sprite = _achievementsIconConfig.GetIcon(achievementId);

            _notificationTween = DOTween.Sequence().Append(
                    _containerParent.DOAnchorPosY(_targetY, _slideDurationSeconds)
                        .SetEase(Ease.InOutSine))
                .AppendInterval(_holdDelaySeconds)
                .Append(
                    _containerParent.DOAnchorPosY(_startY, _slideDurationSeconds)
                        .SetEase(Ease.InOutSine));
        }
    }
}
