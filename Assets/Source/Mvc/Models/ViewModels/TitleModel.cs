using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Models.ViewModels
{
    public class TitleModel
    {
        public BoolReactiveProperty IsFirstStart;

        public TitleModel(SavegameService savegameService)
        {
            var profile = savegameService.Profile;

            IsFirstStart = profile.IsFirstStart;
        }
    }
}
