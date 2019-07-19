using System;
using Assets.Source.App;
using Assets.Source.Features.Upgrades.Data;
using UnityEngine;

namespace Assets.Source.Services
{
    public static class TextService
    {
        private const string MissingText = "#MISSING TEXT#";
        private const string InlineSpriteCoin = "<sprite=0>";
        private const string InlineSpriteExternalLink = "<sprite=1>";

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

        internal static string ResetProfileDescription()
        {
            return "Are you sure?\n\nThis will reset all your upgrades, ingame-money and records.";
        }

        internal static string ResetProfileWarning()
        {
            return "This cannot be undone!";
        }

        internal static string Cancel()
        {
            return "Cancel";
        }

        public static string Max()
        {
            return "MAX";
        }

        public static string LevelX(int level)
        {
            return $"Level {level}";
        }

        internal static string HowToPlay()
        {
            return "How to Play";
        }

        internal static string Credits()
        {
            return "Credits";
        }

        internal static string DistanceReached()
        {
            return "Distance Reached";
        }

        internal static string NewBest()
        {
            return "New Best!";
        }

        public static string CurrencyAmount(int amount)
        {
            return $"{amount}{InlineSpriteCoin}";
        }

        public static string CurrencyAmount(float amount)
        {
            return CurrencyAmount(Mathf.RoundToInt(amount));
        }

        public static string PlayAgainExclamation()
        {
            return "Play Again!";
        }

        public static string PlayExclamation()
        {
            return "Play!";
        }

        public static string RestoreDefaults()
        {
            return "Restore Defaults";
        }

        public static string ResetProfile()
        {
            return "Reset Profile";
        }

        internal static string Settings()
        {
            return "Settings";
        }

        internal static string SoundSettings()
        {
            return "Sound Settings";
        }

        internal static string SoundEffects()
        {
            return "Sound Effects";
        }

        internal static string Music()
        {
            return "Music";
        }
    }
}