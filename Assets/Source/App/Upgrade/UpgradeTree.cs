using System.Collections.Generic;

namespace Assets.Source.App.Upgrade
{
    static class UpgradeTree
    {
        public static readonly UpgradePath<float> MaxVelocityPath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 30f },
                new UpgradeStep<float> { Cost = 150, Value = 40f },
                new UpgradeStep<float> { Cost = 300, Value = 50f },
                new UpgradeStep<float> { Cost = 600, Value = 70f },
                new UpgradeStep<float> { Cost = 1800, Value = 110f },
                new UpgradeStep<float> { Cost = 4000, Value = 150f },
                new UpgradeStep<float> { Cost = 10000, Value = 200f }
            }
        );

        public static readonly UpgradePath<float> KickForcePath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 13f },
                new UpgradeStep<float> { Cost = 300, Value = 15f },
                new UpgradeStep<float> { Cost = 650, Value = 20f },
                new UpgradeStep<float> { Cost = 1500, Value = 25f },
                new UpgradeStep<float> { Cost = 3000, Value = 40f },
                new UpgradeStep<float> { Cost = 7000, Value = 60f },
                new UpgradeStep<float> { Cost = 15000, Value = 80f }
            }
        );

        public static readonly UpgradePath<float> ShootForcePath = new UpgradePath<float>(
            new List<UpgradeStep<float>>()
            {
                new UpgradeStep<float> { Cost = 0, Value = 8f },
                new UpgradeStep<float> { Cost = 500, Value = 10f },
                new UpgradeStep<float> { Cost = 1000, Value = 15f },
                new UpgradeStep<float> { Cost = 2000, Value = 20f },
                new UpgradeStep<float> { Cost = 3000, Value = 28f }
            }
        );

        public static readonly UpgradePath<int> ShootCountPath = new UpgradePath<int>(
            new List<UpgradeStep<int>>()
            {
                new UpgradeStep<int> { Cost = 0, Value = 3 },
                new UpgradeStep<int> { Cost = 200, Value = 4 },
                new UpgradeStep<int> { Cost = 600, Value = 5 },
                new UpgradeStep<int> { Cost = 2000, Value = 6 },
                new UpgradeStep<int> { Cost = 4000, Value = 7 },
                new UpgradeStep<int> { Cost = 8000, Value = 8 },
                new UpgradeStep<int> { Cost = 10000, Value = 9 }
            }
        );
    }
}
