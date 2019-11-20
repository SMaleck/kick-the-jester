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
        public IReadOnlyReactiveProperty<float> BestDistance => _profileSavegame.BestDistance;
        public IReadOnlyReactiveProperty<int> RoundsPlayed => _profileSavegame.RoundsPlayed;

        public IReadOnlyReactiveProperty<bool> HasCompletedTutorial => _profileSavegame.IsFirstStart;

        public PlayerProfileModel(SavegameService savegameService)
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

        public void SetBestDistance(float distance)
        {
            var currentBestDistance = _profileSavegame.BestDistance.Value;
            _profileSavegame.BestDistance.Value = Math.Max(currentBestDistance, distance);
        }

        public void IncrementRoundsPlayed()
        {
            _profileSavegame.RoundsPlayed.Value += 1;
        }

        public void SetHasCompletedTutorial(bool value)
        {
            _profileSavegame.IsFirstStart.Value = value;
        }
    }
}
