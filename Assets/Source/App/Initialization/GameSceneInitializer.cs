using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.RequirementStrategies;
using Assets.Source.Features.Cheats;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Assets.Source.Services.Savegames;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Source.App.Initialization
{
    public class GameSceneInitializer : AbstractSceneInitializer, IInitializable
    {
        [Inject] private readonly ISavegameService _savegameService;

        [Inject] private readonly BestDistanceMarkerView _bestDistanceMarkerView;
        [Inject] private readonly HudView _hudView;
        [Inject] private readonly PauseView _pauseView;
        [Inject] private readonly ResetProfileConfirmationView _resetProfileConfirmationView;
        [Inject] private readonly RoundEndView _roundEndView;
        [Inject] private readonly SettingsView _settingsView;
        [Inject] private readonly UpgradeScreenView _upgradeScreenView;
        [Inject] private readonly CheatView _cheatView;

        [Inject] private readonly AchievementModel.Factory _achievementModelFactory;
        [Inject] private readonly RequirementStrategyFactory _requirementStrategyFactory;
        [Inject] private readonly AchievementUnlockController.Factory _achievementUnlockControllerFactory;


        public void Initialize()
        {
            InitViews();
            InitAchievements();
        }

        private void InitViews()
        {
            SetupView(_bestDistanceMarkerView);
            SetupView(_hudView);

            SetupClosableView(_roundEndView, ClosableViewType.RoundEnd);
            SetupClosableView(_pauseView, ClosableViewType.Pause);
            SetupClosableView(_settingsView, ClosableViewType.Settings);
            SetupClosableView(_resetProfileConfirmationView, ClosableViewType.ResetProfileConfirmation);
            SetupClosableView(_upgradeScreenView, ClosableViewType.Upgrades);

            SetupClosableView(_cheatView);
        }


        private void InitAchievements()
        {
            var achievementSavegames = _savegameService
                .Savegame
                .AchievementsSavegame
                .AchievementSavegames
                .ToDictionary(
                    savegame => savegame.Id,
                    savegame => savegame);

            var achievementRequirementStrategies = new List<IRequirementStrategy>();

            EnumHelper<AchievementId>.ForEach(id =>
            {
                if (!achievementSavegames.TryGetValue(id, out var savegame))
                {
                    throw new NullReferenceException($"No savegame found for AchievementId {id}");
                }

                var model = _achievementModelFactory.Create(savegame);

                var requirementStrategy = _requirementStrategyFactory.CreateRequirementStrategy(model);
                achievementRequirementStrategies.Add(requirementStrategy);
            });

            _achievementUnlockControllerFactory.Create(achievementRequirementStrategies);
        }
    }
}
