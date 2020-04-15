using Assets.Source.Features.PlayerData;
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
        private readonly GameRoundStatsModel _gameRoundStatsModel;
        private readonly SceneTransitionService _sceneTransitionService;

        public CheatController(
            CheatView cheatView,
            PlayerProfileModel playerProfileModel,
            GameRoundStatsModel gameRoundStatsModel,
            SceneTransitionService sceneTransitionService)
        {
            cheatView.Initialize();

            _playerProfileModel = playerProfileModel;
            _gameRoundStatsModel = gameRoundStatsModel;
            _sceneTransitionService = sceneTransitionService;

            if (!Debug.isDebugBuild)
            {
                return;
            }

            Observable.EveryUpdate()
                .Subscribe(_ => CheckAllCheatKeys())
                .AddTo(Disposer);

            cheatView.OnGiveMuchCurrencyClicked
                .Subscribe(_ => GiveMuchCurrency())
                .AddTo(Disposer);

            cheatView.OnGiveMuchCurrencyClicked
                .Subscribe(_ => GiveMuchCurrency())
                .AddTo(Disposer);

            cheatView.OnSlowTimeClicked
                .Subscribe(_ => SlowTime())
                .AddTo(Disposer);

            cheatView.OnAddPickupClicked
                .Subscribe(_ => AddPickup())
                .AddTo(Disposer);

            cheatView.OnAddProjectileClicked
                .Subscribe(_ => AddProjectiles())
                .AddTo(Disposer);

            cheatView.OnSwitchLanguageClicked
                .Subscribe(_ => SwitchLanguage())
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
            _gameRoundStatsModel.AddGains(AddPickupAmount);
        }

        private void AddProjectiles()
        {
            var currentProjectileCount = _gameRoundStatsModel.ShotsRemaining.Value;
            _gameRoundStatsModel.SetRemainingShotsIfHigher(currentProjectileCount + AddProjectilesAmount);
        }

        private void SwitchLanguage()
        {
            var currentLanguage = TextService.CurrentLanguage;
            var nextLanguage = currentLanguage + 1;

            if (!Enum.IsDefined(typeof(Language), nextLanguage))
            {
                nextLanguage = default;
            }

            TextService.SetLanguage(nextLanguage);
            _sceneTransitionService.ToTitle();
        }
    }
}
