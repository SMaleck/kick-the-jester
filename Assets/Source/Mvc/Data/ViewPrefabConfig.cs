﻿using Assets.Source.App;
using Assets.Source.Mvc.Views;
using UnityEngine;

namespace Assets.Source.Mvc.Data
{
    [CreateAssetMenu(fileName = "ViewPrefabConfig", menuName = Constants.PROJECT_MENU_ROOT + "/ViewPrefabConfig")]
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

        [Header("Upgrades")]
        [SerializeField] private UpgradeScreenView _upgradeScreenViewPrefab;
        public UpgradeScreenView UpgradeScreenViewPrefab => _upgradeScreenViewPrefab;

        [SerializeField] private UpgradeItemView _upgradeItemViewPrefab;
        public UpgradeItemView UpgradeItemViewPrefab => _upgradeItemViewPrefab;
    }
}
