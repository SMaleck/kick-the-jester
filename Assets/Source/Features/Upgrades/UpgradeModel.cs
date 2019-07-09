using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeModel : AbstractDisposable
    {
        public readonly UpgradePathType UpgradePathType;
        private readonly UpgradeTreeConfig.UpgradePath _upgradePath;

        private readonly ReactiveProperty<int> _level;
        public IReadOnlyReactiveProperty<int> Level => _level;

        private readonly ReactiveProperty<int> _cost;
        public IReadOnlyReactiveProperty<int> Cost => _cost;

        private readonly ReactiveProperty<float> _value;
        public IReadOnlyReactiveProperty<float> Value => _value;

        private readonly ReactiveProperty<bool> _isMaxed;
        public IReadOnlyReactiveProperty<bool> IsMaxed => _isMaxed;

        public UpgradeModel(
            UpgradePathType upgradeType,
            UpgradeTreeConfig data,
            ReactiveProperty<int> level)
        {
            UpgradePathType = upgradeType;
            _level = level;

            _cost = new ReactiveProperty<int>().AddTo(Disposer);
            _value = new ReactiveProperty<float>().AddTo(Disposer);
            _isMaxed = new ReactiveProperty<bool>().AddTo(Disposer);

            _upgradePath = data.GetUpgradePath(UpgradePathType);
            UpdateUpgradeValues();
        }

        public void SetLevel(int level)
        {
            _level.Value = Math.Min(_upgradePath.MaxLevel, level);
            UpdateUpgradeValues();
        }

        private void UpdateUpgradeValues()
        {
            _value.Value = _upgradePath.Steps[_level.Value].Value;

            var isMaxed = _level.Value >= _upgradePath.MaxLevel;
            _isMaxed.Value = isMaxed;
            _cost.Value = isMaxed ? 0 : _upgradePath.Steps[_level.Value + 1].Cost;
        }
    }
}
