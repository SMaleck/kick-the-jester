using System.Collections.Generic;

namespace Assets.Source.App.Upgrade
{
    static class UpgradeTree
    {
        public static readonly UpgradePath<float> MaxVelocityPath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 50f },
                new UpgradeStep<float> { Cost = 150, Value = 75f },
                new UpgradeStep<float> { Cost = 200, Value = 100f }
            }
        );

        public static readonly UpgradePath<float> KickForcePath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 17f },
                new UpgradeStep<float> { Cost = 400, Value = 21f },
                new UpgradeStep<float> { Cost = 800, Value = 35f }
            }
        );

        public static readonly UpgradePath<float> ShootForcePath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 8f },
                new UpgradeStep<float> { Cost = 400, Value = 10f },
                new UpgradeStep<float> { Cost = 800, Value = 12f }
            }
        );

        public static readonly UpgradePath<int> ShootCountPath = new UpgradePath<int>(
            new List<UpgradeStep<int>>()
            {
                new UpgradeStep<int> { Cost = 0, Value = 3 },
                new UpgradeStep<int> { Cost = 400, Value = 4 },
                new UpgradeStep<int> { Cost = 800, Value = 5 }
            }
        );
    }
}
