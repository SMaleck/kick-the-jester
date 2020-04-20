using Assets.Source.Features.Achievements;
using Assets.Source.Mvc.Data;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class AchievementsScreenController : AbstractDisposable
    {
        private readonly AchievementsScreenView _achievementsScreenView;
        private readonly ViewPrefabConfig _viewPrefabConfig;
        private readonly AchievementItemView.Factory _achievementItemViewFactory;
        private readonly List<AchievementModel> _achievementModels;

        public AchievementsScreenController(
            AchievementsScreenView achievementsScreenView,
            ViewPrefabConfig viewPrefabConfig,
            AchievementItemView.Factory achievementItemViewFactory,
            List<AchievementModel> achievementModels)
        {
            _achievementsScreenView = achievementsScreenView;
            _viewPrefabConfig = viewPrefabConfig;
            _achievementItemViewFactory = achievementItemViewFactory;
            _achievementModels = achievementModels;

            CreateItemViews();
        }

        private void CreateItemViews()
        {
            _achievementModels
                .OrderBy(model => model.RequirementType)
                .ThenBy(model => model.Id)
                .ToList()
                .ForEach(CreateItemView);
        }

        private void CreateItemView(AchievementModel achievementModel)
        {
            var view = _achievementItemViewFactory.Create(_viewPrefabConfig.AchievementItemViewPrefab);
            view.SetParent(_achievementsScreenView.AchievementItemsParent);

            view.SetAchievementId(achievementModel.Id);

            view.SetRequirements(
                achievementModel.RequirementType,
                achievementModel.Requirement);

            achievementModel.IsUnlocked
                .Subscribe(view.SetIsUnlocked)
                .AddTo(Disposer);
        }
    }
}
