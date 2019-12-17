using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Util;
using System;
using UnityEngine;

namespace Assets.Source.Services.Localization
{
    public static class TextService
    {
        private const string InlineSpriteCoin = "<sprite=0>";
        private const string InlineSpriteExternalLink = "<sprite=1>";

        public static Language CurrentLanguage => TextRepo.CurrentLanguage;

        public static void SetLanguage(Language language)
        {
            TextRepo.SetLanguage(language);
        }

        public static string UpgradeItemTitle(UpgradePathType upgradePathType)
        {
            switch (upgradePathType)
            {
                case UpgradePathType.KickForce:
                    return TextRepo.GetText(TextKey.UpgradePathKickForceTitle);

                case UpgradePathType.ShootForce:
                    return TextRepo.GetText(TextKey.UpgradePathShootForceTitle);

                case UpgradePathType.ProjectileCount:
                    return TextRepo.GetText(TextKey.UpgradePathProjectileCountTitle);

                case UpgradePathType.VelocityCap:
                    return TextRepo.GetText(TextKey.UpgradePathVelocityCapTitle);

                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradePathType), upgradePathType, null);
            }
        }

        public static string UpgradeItemDescription(UpgradePathType upgradePathType)
        {
            switch (upgradePathType)
            {
                case UpgradePathType.KickForce:
                    return TextRepo.GetText(TextKey.UpgradePathKickForceDescription);

                case UpgradePathType.ShootForce:
                    return TextRepo.GetText(TextKey.UpgradePathShootForceDescription);

                case UpgradePathType.ProjectileCount:
                    return TextRepo.GetText(TextKey.UpgradePathProjectileCountDescription);

                case UpgradePathType.VelocityCap:
                    return TextRepo.GetText(TextKey.UpgradePathVelocityCapDescription);

                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradePathType), upgradePathType, null);
            }
        }

        public static string ResetProfileDescription()
        {
            return TextRepo.GetText(TextKey.ResetProfileDescription);
        }

        public static string ResetProfileWarning()
        {
            return TextRepo.GetText(TextKey.ResetProfileWarning);
        }

        public static string Cancel()
        {
            return TextRepo.GetText(TextKey.Cancel);
        }

        public static string Max()
        {
            return TextRepo.GetText(TextKey.Max);
        }

        public static string LevelX(int level)
        {
            return $"{TextRepo.GetText(TextKey.Level)} {level}";
        }

        public static string HowToPlay()
        {
            return TextRepo.GetText(TextKey.HowToPlay);
        }

        public static string Credits()
        {
            return TextRepo.GetText(TextKey.Credits);
        }

        public static string DistanceReached()
        {
            return TextRepo.GetText(TextKey.DistanceReached);
        }

        public static string NewBest()
        {
            return TextRepo.GetText(TextKey.NewBest);
        }

        public static string BestLabel()
        {
            return TextRepo.GetText(TextKey.BestLabel);
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
            return TextRepo.GetText(TextKey.PlayAgainExclamation);
        }

        public static string PlayExclamation()
        {
            return TextRepo.GetText(TextKey.PlayExclamation);
        }

        public static string RestoreDefaults()
        {
            return TextRepo.GetText(TextKey.RestoreDefaults);
        }

        public static string ResetProfile()
        {
            return TextRepo.GetText(TextKey.ResetProfile);
        }

        public static string Settings()
        {
            return TextRepo.GetText(TextKey.Settings);
        }

        public static string SoundSettings()
        {
            return TextRepo.GetText(TextKey.SoundSettings);
        }

        public static string SoundEffects()
        {
            return TextRepo.GetText(TextKey.SoundEffects);
        }

        public static string Music()
        {
            return TextRepo.GetText(TextKey.Music);
        }

        public static string CreditsCodeHeader()
        {
            return TextRepo.GetText(TextKey.CreditsCodeHeader);
        }

        public static string CreditsArtHeader()
        {
            return TextRepo.GetText(TextKey.CreditsArtHeader);
        }

        public static string CreditsMusicHeader()
        {
            return TextRepo.GetText(TextKey.CreditsMusicHeader);
        }

        public static string CreditsSoundEffectsHeader()
        {
            return TextRepo.GetText(TextKey.CreditsSoundEffectsHeader);
        }

        public static string BestDistance()
        {
            return TextRepo.GetText(TextKey.BestDistance);
        }

        public static string MetersAmount(float amount)
        {
            return $"{amount.ToMeters()}m";
        }

        public static string FromCurrencyGainType(CurrencyGainType currencyGainType)
        {
            switch (currencyGainType)
            {
                case CurrencyGainType.Distance:
                    return TextRepo.GetText(TextKey.CurrencyGainTypeDistance);

                case CurrencyGainType.Pickup:
                    return TextRepo.GetText(TextKey.CurrencyGainTypePickup);

                case CurrencyGainType.ShortDistanceBonus:
                    return TextRepo.GetText(TextKey.CurrencyGainTypeShortDistanceBonus);

                case CurrencyGainType.MaxHeightBonus:
                    return TextRepo.GetText(TextKey.CurrencyGainTypeMaxHeightBonus);

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
            return TextRepo.GetText(TextKey.TutorialStepOne);
        }

        public static string TutorialStepTwo()
        {
            return TextRepo.GetText(TextKey.TutorialStepTwo);
        }

        public static string TutorialStepThree()
        {
            return TextRepo.GetText(TextKey.TutorialStepThree);
        }

        public static string TutorialStepThreeTip()
        {
            return TextRepo.GetText(TextKey.TutorialStepThreeTip);
        }

        public static string TapAnywhereToContinue()
        {
            return TextRepo.GetText(TextKey.TapAnywhereToContinue);
        }

        public static string Pause()
        {
            return TextRepo.GetText(TextKey.Pause);
        }

        public static string Language()
        {
            return TextRepo.GetText(TextKey.Language);
        }

        public static string LanguageName(Language language)
        {
            return TextRepo.GetLanguageText(language);
        }

        public static string Restart()
        {
            return TextRepo.GetText(TextKey.Restart);
        }

        public static string Continue()
        {
            return TextRepo.GetText(TextKey.Continue);
        }

        public static string Upgrades()
        {
            return TextRepo.GetText(TextKey.Upgrades);
        }
    }
}