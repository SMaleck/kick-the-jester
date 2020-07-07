using Assets.Source.Features.Achievements;
using Assets.Source.Mvc.Views;
using Assets.Source.Util;
using System.Collections.Generic;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class AchievementNotificationController : AbstractDisposable
    {
        public AchievementNotificationController(
            AchievementNotificationView achievementNotificationView,
            List<AchievementModel> achievementModels)
        {
            achievementNotificationView.Setup();

            achievementModels.ForEach(model =>
            {
                model.IsUnlocked
                    .Pairwise()
                    .Where(pair => !pair.Previous && pair.Current)
                    .Subscribe(_ => achievementNotificationView.ShowFor(model.Id))
                    .AddTo(Disposer);
            });
        }
    }
}
