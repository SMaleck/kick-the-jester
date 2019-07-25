using System;
using Assets.Source.App;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Util;
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

        public static string ResetProfileDescription()
        {
            return "Are you sure?\n\nThis will reset all your upgrades, ingame-money and records.";
        }        

        public static string ResetProfileWarning()
        {
            return "This cannot be undone!";
        }

        public static string Cancel()
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

        public static string HowToPlay()
        {
            return "How to Play";
        }

        public static string Credits()
        {
            return "Credits";
        }

        public static string DistanceReached()
        {
            return "Distance Reached";
        }

        public static string NewBest()
        {
            return "New Best!";
        }

        public static string CurrencyAmount(int amount)
        {
            return $"{amount}{InlineSpriteCoin}";
        }

        public static string BestLabel()
        {
            return "Best:";
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

        public static string Settings()
        {
            return "Settings";
        }

        public static string SoundSettings()
        {
            return "Sound Settings";
        }

        public static string SoundEffects()
        {
            return "Sound Effects";
        }

        public static string Music()
        {
            return "Music";
        }

        public static string CreditsCodeHeader()
        {
            return "Code & Game Design";
        }

        public static string CreditsArtHeader()
        {
            return "Art & Animation";
        }

        public static string CreditsMusicHeader()
        {
            return "Music";
        }

        public static string CreditsSoundEffectsHeader()
        {
            return "Sound Effects";
        }

        public static string BestDistance()
        {
            return "Best Distance";
        }

        public static string MetersAmount(float amount)
        {
            return $"{amount.ToMeters()}m";
        }

        public static string FromCurrencyGainType(CurrencyGainType currencyGainType)
        {
            switch(currencyGainType)
            {
                case CurrencyGainType.Distance:
                    return "from distance";

                case CurrencyGainType.Pickup:
                    return "from pickups";

                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyGainType), currencyGainType, null);
            }
        }

        public static string TimesAmount(int amount)
        {
            return $"x {amount.ToString()}";
        }

        public static string TutorialStepOne()
        {
            return "Click / Tap anywhere on the screen to kick!";
        }

        public static string TutorialStepTwo()
        {
            return "This is your Kick Power.\nTry to kick when it is full!";
        }

        public static string TutorialStepThree()
        {
            return "Click / Tap anywhere on the screen to throw tomatoes at the Jester.";
        }

        public static string TutorialStepThreeTip()
        {
            return "Tip: They are most effective, when the Jester is on his way up.";
        }

        public static string TapAnywhereToContinue()
        {
            return "Tap anywhere to continue";
        }
    }
}