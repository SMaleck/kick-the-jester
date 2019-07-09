using Assets.Source.Features.Upgrades.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Source.Services
{
    public static class TextService
    {
        private const string MissingText = "#MISSING TEXT#";

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
    }
}