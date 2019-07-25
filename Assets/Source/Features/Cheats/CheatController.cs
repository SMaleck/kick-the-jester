﻿using Assets.Source.Features.PlayerData;
using Assets.Source.Services;
using Assets.Source.Services.Localization;
using Assets.Source.Util;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.Features.Cheats
{
    public class CheatController : AbstractDisposable
    {
        private const string GiveMuchCurrencyKey = "c";
        private const int MuchMoneyAmount = 100000000;

        private const string SlowTimeKey = "t";
        private const float SlowTimeScale = 0.5f;

        private const string AddPickupKey = "v";
        private const int AddPickupAmount = 150;

        private const string AddProjectilesKey = "b";
        private const int AddProjectilesAmount = 5;

        private const string SwitchLanguageKey = "l";

        private readonly PlayerProfileModel _playerProfileModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public CheatController(
            PlayerProfileModel playerProfileModel,
            FlightStatsModel flightStatsModel,
            SceneTransitionService sceneTransitionService)
        {
            _playerProfileModel = playerProfileModel;
            _flightStatsModel = flightStatsModel;
            _sceneTransitionService = sceneTransitionService;

            Observable.EveryUpdate()
                .Subscribe(_ => CheckAllCheatKeys())
                .AddTo(Disposer);
        }

        private void CheckAllCheatKeys()
        {
            CheckOnInput(GiveMuchCurrencyKey, GiveMuchCurrency);
            CheckOnInput(SlowTimeKey, SlowTime);
            CheckOnInput(AddPickupKey, AddPickup);
            CheckOnInput(AddProjectilesKey, AddProjectiles);
            CheckOnInput(SwitchLanguageKey, SwitchLanguage);
        }

        private void CheckOnInput(string keyCode, Action action)
        {
            if (Input.GetKeyDown(keyCode))
            {
                action();
            }
        }

        private void GiveMuchCurrency()
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

        private void AddProjectiles()
        {
            var currentProjectileCount = _flightStatsModel.ShotsRemaining.Value;
            _flightStatsModel.SetRemainingShotsIfHigher(currentProjectileCount + AddProjectilesAmount);
        }

        private void SwitchLanguage()
        {
            var currentLanguage = TextService.CurrentLanguage;
            var nextLanguage = currentLanguage + 1;

            if (!Enum.IsDefined(typeof(Language), nextLanguage))
            {
                nextLanguage = default(Language);
            }

            TextService.SetLanguage(nextLanguage);
            _sceneTransitionService.ToTitle();
        }
    }
}
