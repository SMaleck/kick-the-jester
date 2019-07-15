using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.Cheats
{
    public class CheatController : AbstractDisposable
    {
        private const string GiveMuchMoneyKey = "i";
        private const int MuchMoneyAmount = 100000000;

        private readonly PlayerProfileModel _playerProfileModel;

        public CheatController(PlayerProfileModel playerProfileModel)
        {
            _playerProfileModel = playerProfileModel;

            Observable.EveryUpdate()
                .Subscribe(_ => CheckInput())
                .AddTo(Disposer);
        }

        private void CheckInput()
        {
            if (Input.GetKeyDown(GiveMuchMoneyKey))
            {
                GiveMuchMoney();
            }
        }

        private void GiveMuchMoney()
        {
            _playerProfileModel.AddCurrencyAmount(MuchMoneyAmount);
        }
    }
}
