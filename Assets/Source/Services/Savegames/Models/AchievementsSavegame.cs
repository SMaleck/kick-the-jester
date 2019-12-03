using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Source.Services.Savegames.Models
{
    public class AchievementsSavegameData : AbstractSavegameData
    {
        public List<AchievementSavegameData> AchievementSavegames;
    }

    public class AchievementsSavegame : AbstractSavegame
    {
        public readonly List<AchievementSavegame> AchievementSavegames;

        public AchievementsSavegame(AchievementsSavegameData data)
        {
            AchievementSavegames = data.AchievementSavegames
                .Select(savegame => new AchievementSavegame(savegame))
                .ToList();
        }
    }
}
