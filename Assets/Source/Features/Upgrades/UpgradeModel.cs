using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Services.Savegames.Models;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeModel : AbstractDisposable
    {
        private readonly UpgradeSavegame _upgradeSavegame;
        private readonly UpgradeTreeConfig.UpgradePath _upgradePath;

        public UpgradePathType UpgradePathType => _upgradeSavegame.UpgradePathType.Value;

        public IReadOnlyReactiveProperty<int> Level => _upgradeSavegame.Level;

        private readonly ReactiveProperty<float> _value;
        public IReadOnlyReactiveProperty<float> Value => _value;

        private readonly ReactiveProperty<bool> _isMaxed;
        public IReadOnlyReactiveProperty<bool> IsMaxed => _isMaxed;

        private readonly ReactiveProperty<int> _cost;
        public IReadOnlyReactiveProperty<int> Cost => _cost;

        private readonly ReactiveProperty<bool> _canAfford;
        public IReadOnlyReactiveProperty<bool> CanAfford => _canAfford;

        public UpgradeModel(
            UpgradeSavegame upgradeSavegame,
            UpgradeTreeConfig data)
        {
            _upgradeSavegame = upgradeSavegame;

            _value = new ReactiveProperty<float>().AddTo(Disposer);
            _isMaxed = new ReactiveProperty<bool>().AddTo(Disposer);
            _cost = new ReactiveProperty<int>().AddTo(Disposer);
            _canAfford = new ReactiveProperty<bool>().AddTo(Disposer);

            _upgradePath = data.GetUpgradePath(UpgradePathType);

            Level.Subscribe(_ => UpdateUpgradeValues());
        }

        public void SetLevel(int level)
        {
            _upgradeSavegame.Level.Value = Math.Min(_upgradePath.MaxLevel, level);
        }

        public void SetCanAfford(bool canAfford)
        {
            _canAfford.Value = canAfford;
        }

        private void UpdateUpgradeValues()
        {
            _value.Value = _upgradePath.Steps[Level.Value].Value;

            var isMaxed = Level.Value >= _upgradePath.MaxLevel;
            _isMaxed.Value = isMaxed;
            _cost.Value = isMaxed ? 0 : _upgradePath.Steps[Level.Value + 1].Cost;
        }
    }
}
