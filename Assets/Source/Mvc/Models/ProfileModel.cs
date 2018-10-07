using Assets.Source.Services.Savegame;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class ProfileModel
    {
        public IntReactiveProperty Currency;
        public FloatReactiveProperty BestDistance;
        public IntReactiveProperty RoundsPlayed;


        public ProfileModel(SavegameService savegameService)
        {
            var profile = savegameService.Profile;

            Currency = profile.Currency;
            BestDistance = profile.BestDistance;
            RoundsPlayed = profile.RoundsPlayed;            
        }
    }
}
