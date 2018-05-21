using System.Collections.Generic;

namespace Assets.Source.App.Upgrades
{
    public class UpgradeStep<T>
    {
        public int Cost;
        public T Value;
    }

    static class UpgradeTree
    {
        public static readonly List<UpgradeStep<float>> MaxVelocity = new List<UpgradeStep<float>>()
        {
            new UpgradeStep<float> { Cost = 100, Value = 50f },
            new UpgradeStep<float> { Cost = 150, Value = 75f },
            new UpgradeStep<float> { Cost = 200, Value = 100f }
        };

        public static readonly List<UpgradeStep<float>> KickForce = new List<UpgradeStep<float>>()
        {
            new UpgradeStep<float> { Cost = 200, Value = 17f },
            new UpgradeStep<float> { Cost = 400, Value = 21f },
            new UpgradeStep<float> { Cost = 800, Value = 35f }
        };

        public static readonly List<UpgradeStep<float>> ShootForce = new List<UpgradeStep<float>>()
        {
            new UpgradeStep<float> { Cost = 200, Value = 8f },
            new UpgradeStep<float> { Cost = 400, Value = 10f },
            new UpgradeStep<float> { Cost = 800, Value = 12f }
        };

        public static readonly List<UpgradeStep<int>> ShootCount = new List<UpgradeStep<int>>()
        {
            new UpgradeStep<int> { Cost = 200, Value = 3 },
            new UpgradeStep<int> { Cost = 400, Value = 4 },
            new UpgradeStep<int> { Cost = 800, Value = 5 }
        };
    }
}
