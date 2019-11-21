using Assets.Source.Util;

namespace Assets.Source.Features.PlayerData
{
    public class PlayerProfileController : AbstractDisposable
    {
        private readonly PlayerProfileModel _playerProfileModel;

        public PlayerProfileController(PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;
        }

        public void AddCurrencyAmount(int amount)
        {
            _playerProfileModel.AddCurrencyAmount(amount);
        }
    }
}
