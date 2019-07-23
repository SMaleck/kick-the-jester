using Assets.Source.Features.PlayerData;
using Assets.Source.Mvc.Models;
using Assets.Source.Util;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.Cheats
{
    public class CheatController : AbstractDisposable
    {
        private const string GiveMuchMoneyKey = "r";
        private const int MuchMoneyAmount = 100000000;

        private const string SlowTimeKey = "t";
        private const float SlowTimeScale = 0.5f;

        private const string AddPickupKey = "e";
        private const int AddPickupAmount = 150;

        private readonly PlayerProfileModel _playerProfileModel;
        private readonly FlightStatsModel _flightStatsModel;

        public CheatController(
            PlayerProfileModel playerProfileModel,
            FlightStatsModel flightStatsModel)
        {
            _playerProfileModel = playerProfileModel;
            _flightStatsModel = flightStatsModel;

            Observable.EveryUpdate()
                .Subscribe(_ => CheckAllCheatKeys())
                .AddTo(Disposer);
        }

        private void CheckAllCheatKeys()
        {
            CheckOnInput(GiveMuchMoneyKey, GiveMuchMoney);
            CheckOnInput(SlowTimeKey, SlowTime);
            CheckOnInput(AddPickupKey, AddPickup);
        }

        private void CheckOnInput(string keyCode, Action action)
        {
            if (Input.GetKeyDown(keyCode))
            {
                action();
            }
        }

        private void GiveMuchMoney()
        {
            _playerProfileModel.AddCurrencyAmount(MuchMoneyAmount);
        }

        private void SlowTime()
        {
            Time.timeScale = Time.timeScale < 1 ? 1 : SlowTimeScale;
        }

        private void AddPickup()
        {
            _flightStatsModel.Gains.Add(AddPickupAmount);
        }
    }
}
