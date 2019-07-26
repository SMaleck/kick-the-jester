using Assets.Source.Services.Savegame;
using Assets.Source.Services.Savegame.StorageModels;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerProfileModel : AbstractDisposable
    {
        private readonly ProfileStorageModel _profileStorageModel;
                
        public IReadOnlyReactiveProperty<int> CurrencyAmount => _profileStorageModel.Currency;      
        public IReadOnlyReactiveProperty<float> BestDistance => _profileStorageModel.BestDistance;
        public IReadOnlyReactiveProperty<int> RoundsPlayed => _profileStorageModel.RoundsPlayed;

        public IReadOnlyReactiveProperty<bool> HasCompletedTutorial => _profileStorageModel.IsFirstStart;

        public PlayerProfileModel(SavegameService savegameService)
        {
            _profileStorageModel = savegameService.Profile;
        }

        public void AddCurrencyAmount(int amount)
        {
            _profileStorageModel.Currency.Value += Math.Abs(amount);
        }

        public void DeductCurrencyAmount(int amount)
        {
            _profileStorageModel.Currency.Value -= Math.Abs(amount);
        }

        public void SetBestDistance(float distance)
        {
            var currentBestDistance = _profileStorageModel.BestDistance.Value;
            _profileStorageModel.BestDistance.Value = Math.Max(currentBestDistance, distance);
        }

        public void IncrementRoundsPlayed()
        {
            _profileStorageModel.RoundsPlayed.Value += 1;
        }

        public void SetHasCompletedTutorial(bool value)
        {
            _profileStorageModel.IsFirstStart.Value = value;
        }
    }
}
