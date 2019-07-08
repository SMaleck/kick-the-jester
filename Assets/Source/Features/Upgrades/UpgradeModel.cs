using System;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Features.Upgrades
{
    public class UpgradeModel : AbstractDisposable
    {
        public readonly UpgradePathType UpgradePathType;
        private readonly UpgradeTreeConfig.UpgradePath _upgradePath;

        private readonly ReactiveProperty<int> _level;
        public IReadOnlyReactiveProperty<int> Level
        {
            get { return _level; }
        }

        private readonly ReactiveProperty<int> _cost;
        public IReadOnlyReactiveProperty<int> Cost
        {
            get { return _cost; }
        }

        private readonly ReactiveProperty<double> _value;
        public IReadOnlyReactiveProperty<double> Value
        {
            get { return _value; }
        }

        private readonly ReactiveProperty<bool> _isMaxed;
        public IReadOnlyReactiveProperty<bool> IsMaxed
        {
            get { return _isMaxed; }
        }

        public UpgradeModel(
            UpgradePathType upgradeType,
            ReactiveProperty<int> level,
            UpgradeTreeConfig data)
        {
            UpgradePathType = upgradeType;
            _level = level;

            _cost = new ReactiveProperty<int>().AddTo(Disposer);
            _value = new ReactiveProperty<double>().AddTo(Disposer);
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
            _cost.Value = _upgradePath.Steps[_level.Value].Cost;
            _value.Value = _upgradePath.Steps[_level.Value].Value;
            _isMaxed.Value = _level.Value >= _upgradePath.MaxLevel;
        }
    }
}
