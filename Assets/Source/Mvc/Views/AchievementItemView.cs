using Assets.Source.Features.Achievements;
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

        public override void Setup()
        {

        }

        public void SetAchievementId(AchievementId achievementId)
        {
            _nameText.text = TextService.AchievementName(achievementId);
        }

        public void SetRequirements(
            AchievementRequirementType achievementRequirementType,
            double requirement)
        {
            _descriptionText.text = TextService.AchievementDescription(
                achievementRequirementType,
                requirement);
        }

        public void SetIsUnlocked(bool isUnlocked)
        {
            _iconImage.gameObject.SetActive(isUnlocked);
        }
    }
}
