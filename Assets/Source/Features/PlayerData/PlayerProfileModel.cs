using Assets.Source.Services.Savegames;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerProfileModel : AbstractDisposable
    {
        private readonly ProfileSavegame _profileSavegame;
                
        public IReadOnlyReactiveProperty<int> CurrencyAmount => _profileSavegame.Currency;

        public IReadOnlyReactiveProperty<bool> HasCompletedTutorial => _profileSavegame.IsFirstStart;

        public PlayerProfileModel(ISavegameService savegameService)
        {
            _profileSavegame = savegameService.Savegame.ProfileSavegame;
        }

        public void AddCurrencyAmount(int amount)
        {
            _profileSavegame.Currency.Value += Math.Abs(amount);
        }

        public void DeductCurrencyAmount(int amount)
        {
            _profileSavegame.Currency.Value -= Math.Abs(amount);
        }

        public void SetHasCompletedTutorial(bool value)
        {
            _profileSavegame.IsFirstStart.Value = value;
        }
    }
}
