using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.Data;
using Assets.Source.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public class AchievementItemView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, AchievementItemView> { }

        [SerializeField] private Image _frameImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Header("Progress")]
        [SerializeField] private GameObject _progressParent;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TextMeshProUGUI _progressText;

        private AchievementsIconConfig _achievementsIconConfig;
        private float _requirement;

        [Inject]
        private void Inject(AchievementsIconConfig achievementsIconConfig)
        {
            _achievementsIconConfig = achievementsIconConfig;
        }

        public override void Setup()
        {
        }

        public void SetAchievementId(AchievementId achievementId)
        {
            _iconImage.sprite = _achievementsIconConfig.GetIcon(achievementId);
            _nameText.text = TextService.AchievementName(achievementId);
            SetIsProgressVisible(achievementId != AchievementId.ReachedMoon);
        }

        public void SetRequirements(
            AchievementRequirementType achievementRequirementType,
            double requirement)
        {
            _requirement = (float)requirement;
            _progressSlider.maxValue = _requirement;

            _descriptionText.text = TextService.AchievementDescription(
                achievementRequirementType,
                requirement);
        }

        public void SetProgress(double current)
        {
            _progressText.text = TextService.XOfY(
                current,
                _requirement);

            _progressSlider.value = (float)current;
        }

        public void SetIsUnlocked(bool isUnlocked)
        {
            _iconImage.gameObject.SetActive(isUnlocked);
        }

        private void SetIsProgressVisible(bool isVisible)
        {
            _progressParent.SetActive(isVisible);
        }
    }
}
