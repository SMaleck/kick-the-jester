using Assets.Source.App;
using Assets.Source.Mvc.Views;
using Assets.Source.Mvc.Views.PartialViews;
using UnityEngine;

namespace Assets.Source.Mvc.Data
{
    [CreateAssetMenu(fileName = nameof(ViewPrefabConfig), menuName = Constants.UMenuRoot + nameof(ViewPrefabConfig))]
    public class ViewPrefabConfig : ScriptableObject
    {
        [Header("Title")]
        [SerializeField] private TitleView _titleViewPrefab;
        public TitleView TitleViewPrefab => _titleViewPrefab;

        [SerializeField] private SettingsView _settingsViewPrefab;
        public SettingsView SettingsViewPrefab => _settingsViewPrefab;

        [SerializeField] private CreditsView _creditsViewPrefab;
        public CreditsView CreditsViewPrefab => _creditsViewPrefab;

        [SerializeField] private TutorialView _tutorialViewPrefab;
        public TutorialView TutorialViewPrefab => _tutorialViewPrefab;

        [Header("Game")]
        [SerializeField] private HudView _hudViewPrefab;
        public HudView HudViewPrefab => _hudViewPrefab;

        [SerializeField] private PauseView _pauseViewPrefab;
        public PauseView PauseViewPrefab => _pauseViewPrefab;

        [SerializeField] private RoundEndView _roundEndViewPrefab;
        public RoundEndView RoundEndViewPrefab => _roundEndViewPrefab;

        [SerializeField] private BestDistanceMarkerView _bestDistanceMarkerViewPrefab;
        public BestDistanceMarkerView BestDistanceMarkerViewPrefab => _bestDistanceMarkerViewPrefab;

        [SerializeField] private CurrencyGainItemView _currencyGainItemViewPrefab;
        public CurrencyGainItemView CurrencyGainItemViewPrefab => _currencyGainItemViewPrefab;

        [SerializeField] private PickupFeedbackView _pickupFeedbackViewPrefab;
        public PickupFeedbackView PickupFeedbackViewPrefab => _pickupFeedbackViewPrefab;

        [Header("Upgrades")]
        [SerializeField] private UpgradeScreenView _upgradeScreenViewPrefab;
        public UpgradeScreenView UpgradeScreenViewPrefab => _upgradeScreenViewPrefab;

        [SerializeField] private UpgradeItemView _upgradeItemViewPrefab;
        public UpgradeItemView UpgradeItemViewPrefab => _upgradeItemViewPrefab;

        [SerializeField] private ResetProfileConfirmationView _resetProfileConfirmationViewPrefab;
        public ResetProfileConfirmationView ResetProfileConfirmationViewPrefab => _resetProfileConfirmationViewPrefab;

        [Header("Achievements")]
        [SerializeField] private AchievementsScreenView _achievementsScreenViewPrefab;
        public AchievementsScreenView AchievementsScreenViewPrefab => _achievementsScreenViewPrefab;

        [SerializeField] private AchievementItemView _achievementItemViewPrefab;
        public AchievementItemView  AchievementItemViewPrefab => _achievementItemViewPrefab;
    }
}
