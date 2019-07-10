using Assets.Source.App;
using Assets.Source.Features.Upgrades.Data;

namespace Assets.Source.Services
{
    public static class TextService
    {
        private const string MissingText = "#MISSING TEXT#";
        private const string InlineSpriteCoin = "<sprite=0>";

        public static string UpgradeItemTitle(UpgradePathType upgradePathType)
        {
            switch (upgradePathType)
            {
                case UpgradePathType.KickForce:
                    return "Knight's Boots";

                case UpgradePathType.ShootForce:
                    return "Tomato Firmness";

                case UpgradePathType.ProjectileCount:
                    return "Tomato Reserves";

                case UpgradePathType.VelocityCap:
                    return "Aerodynamic Costume";

                default:
                    return MissingText;
            }
        }

        public static string UpgradeItemDescription(UpgradePathType upgradePathType)
        {
            switch (upgradePathType)
            {
                case UpgradePathType.KickForce:
                    return "Heavier boots mean stronger kicks. The Jester will launch stronger!";

                case UpgradePathType.ShootForce:
                    return "Firm tomatoes give the Jester that extra kick he needs.";

                case UpgradePathType.ProjectileCount:
                    return "Increases the amount of tomatoes you can throw in one run.";

                case UpgradePathType.VelocityCap:
                    return "Allows the Jester to reach higher speeds!";

                default:
                    return MissingText;
            }
        }

        public static string Max()
        {
            return "MAX";
        }

        public static string LevelX(int level)
        {
            return $"Level {level}";
        }

        public static string CurrencyAmount(int amount)
        {
            return $"{amount}{InlineSpriteCoin}";
        }
    }
}