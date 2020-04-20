using Assets.Source.Services.Localization;
using TMPro;
using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class AchievementsScreenView : AbstractView
    {
        [SerializeField] private TextMeshProUGUI _titleText;

        [SerializeField] private Transform _achievementItemsParent;
        public Transform AchievementItemsParent => _achievementItemsParent;

        public override void Setup()
        {
            _titleText.text = TextService.Achievements();
        }
    }
}
