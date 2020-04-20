using Assets.Source.Entities.Items;
using Assets.Source.Features.Achievements;
using Assets.Source.Features.Achievements.RequirementStrategies;
using Assets.Source.Mvc.Mediation;
using Assets.Source.Mvc.Views;
using Assets.Source.Mvc.Views.PartialViews;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System.Collections.Generic;
using Zenject;

namespace Assets.Source.App.Installers
{
    public class FactoryInstaller : Installer<FactoryInstaller>
    {
        public override void InstallBindings()
        {
            #region WORLD OBJECTS

            Container.BindPrefabFactory<AbstractItemEntity, AbstractItemEntity.Factory>();

            #endregion

            #region UPGRADES

            Container.BindPrefabFactory<UpgradeItemView, UpgradeItemView.Factory>();

            #endregion

            #region ACHIEVEMENTS

            Container.BindFactory<AchievementSavegame, AchievementModel, AchievementModel.Factory>().AsSingle();
            Container.BindFactory<List<IRequirementStrategy>, AchievementUnlockController, AchievementUnlockController.Factory>().AsSingle();

            Container.BindInterfacesAndSelfTo<RequirementStrategyFactory>().AsSingle();
            Container.BindFactory<AchievementModel, ReachedMoonRequirementStrategy, ReachedMoonRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, TotalDistanceRequirementStrategy, TotalDistanceRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, BestDistanceRequirementStrategy, BestDistanceRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, BestHeightRequirementStrategy, BestHeightRequirementStrategy.Factory>().AsSingle();
            Container.BindFactory<AchievementModel, RoundsPlayedRequirementStrategy, RoundsPlayedRequirementStrategy.Factory>().AsSingle();

            Container.BindPrefabFactory<AchievementItemView, AchievementItemView.Factory>();

            #endregion

            #region MISC

            Container.BindFactory<IClosableView, ClosableViewController, ClosableViewController.Factory>();
            Container.BindPrefabFactory<PickupFeedbackView, PickupFeedbackView.Factory>();
            Container.BindPrefabFactory<CurrencyGainItemView, CurrencyGainItemView.Factory>();

            #endregion
        }
    }
}
