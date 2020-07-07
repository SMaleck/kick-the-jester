using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.RequirementStrategies;
using Assets.Source.Services.Savegames;
using Assets.Source.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Source.Mvc.Controllers;
using Zenject;

namespace Assets.Source.App.Installers.FeatureInstallers
{
    public class AchievementsInstaller : Installer<AchievementsInstaller>
    {
        [Inject] private readonly ISavegameService _savegameService;

        [Inject] private readonly AchievementModel.Factory _achievementModelFactory;
        [Inject] private readonly RequirementStrategyFactory _requirementStrategyFactory;
        [Inject] private readonly AchievementUnlockController.Factory _achievementUnlockControllerFactory;

        public override void InstallBindings()
        {
            var achievementSavegames = _savegameService
                .Savegame
                .AchievementsSavegame
                .AchievementSavegames
                .ToDictionary(
                    savegame => savegame.Id,
                    savegame => savegame);

            var achievementRequirementStrategies = new List<IRequirementStrategy>();

            var achievementModels = EnumHelper<AchievementId>.Iterator
                .Select(id =>
                {
                    if (!achievementSavegames.TryGetValue(id, out var savegame))
                    {
                        throw new NullReferenceException($"No savegame found for AchievementId {id}");
                    }

                    var model = _achievementModelFactory.Create(savegame);

                    var requirementStrategy = _requirementStrategyFactory.CreateRequirementStrategy(model);
                    achievementRequirementStrategies.Add(requirementStrategy);

                    return model;
                })
                .ToList();

            _achievementUnlockControllerFactory.Create(achievementRequirementStrategies);

            Container.BindInstances(achievementModels);
            Container.BindInterfacesAndSelfTo<AchievementsScreenController>().AsSingleNonLazy();
            Container.BindInterfacesAndSelfTo<AchievementNotificationController>().AsSingleNonLazy();
        }
    }
}
