using Assets.Source.App;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Services.Localization
{
    public static class TextRepo
    {
        public static Language CurrentLanguage { get; private set; }

        static TextRepo()
        {
            var storedLanguage = PlayerPrefs.GetInt(Constants.PREFS_KEY_LANGUAGE);

            var isStoredLanguageValid = Enum.IsDefined(typeof(Language), storedLanguage);
            if (isStoredLanguageValid)
            {
                CurrentLanguage = (Language)storedLanguage;
            }
            else
            {
                SetLanguage(Language.English);
            }
        }

        public static void SetLanguage(Language language)
        {
            PlayerPrefs.SetInt(Constants.PREFS_KEY_LANGUAGE, (int)language);
            CurrentLanguage = language;
        }

        public static string GetText(TextKey textKey)
        {
            switch (CurrentLanguage)
            {
                case Language.English:
                    return _textsEN[textKey];

                case Language.German:
                    return _textsDE[textKey];

                default:
                    throw new ArgumentOutOfRangeException(nameof(CurrentLanguage), CurrentLanguage, null);
            }
        }

        private static Dictionary<TextKey, string> _textsEN = new Dictionary<TextKey, string>
        {
            { TextKey.PlayExclamation, "Play!"},
            { TextKey.PlayAgainExclamation, "Play Again!"},
            { TextKey.Cancel, "Cancel"},
            { TextKey.UpgradePathKickForceTitle, "Knight's Boots"},
            { TextKey.UpgradePathKickForceDescription, "Heavier boots mean stronger kicks. The Jester will launch stronger!"},
            { TextKey.UpgradePathShootForceTitle, "Tomato Firmness"},
            { TextKey.UpgradePathShootForceDescription, "Firm tomatoes give the Jester that extra kick he needs."},
            { TextKey.UpgradePathProjectileCountTitle, "Tomato Reserves"},
            { TextKey.UpgradePathProjectileCountDescription, "Increases the amount of tomatoes you can throw in one run."},
            { TextKey.UpgradePathVelocityCapTitle, "Aerodynamic Costume"},
            { TextKey.UpgradePathVelocityCapDescription, "Allows the Jester to reach higher speeds!"},
            { TextKey.ResetProfileDescription, "Are you sure?\n\nThis will reset all your upgrades, ingame-money and records."},
            { TextKey.ResetProfileWarning, "This cannot be undone!"},
            { TextKey.Max, "Max"},
            { TextKey.Level, "Level"},
            { TextKey.DistanceReached, "Distance Reached"},
            { TextKey.NewBest, "New Best!"},
            { TextKey.BestLabel, "Best:"},
            { TextKey.BestDistance, "Best Distance"},
            { TextKey.CurrencyGainTypeDistance, "from distance"},
            { TextKey.CurrencyGainTypePickup, "from pickups"},
            { TextKey.RestoreDefaults, "Restore Defaults"},
            { TextKey.ResetProfile, "Reset Profile"},
            { TextKey.Settings, "Settings"},
            { TextKey.SoundSettings, "Sound Settings"},
            { TextKey.SoundEffects, "Sound Effects"},
            { TextKey.Music, "Music"},
            { TextKey.Credits, "Credits"},
            { TextKey.CreditsCodeHeader, "Code & Game Design"},
            { TextKey.CreditsArtHeader, "Art & Animation"},
            { TextKey.CreditsMusicHeader, "Music"},
            { TextKey.CreditsSoundEffectsHeader, "Sound Effects"},
            { TextKey.HowToPlay, "How to Play"},
            { TextKey.TutorialStepOne, "Click / Tap anywhere on the screen to kick!"},
            { TextKey.TutorialStepTwo, "This is your Kick Power.\nTry to kick when it is full!"},
            { TextKey.TutorialStepThree, "Click / Tap anywhere on the screen to throw tomatoes at the Jester."},
            { TextKey.TutorialStepThreeTip, "Tip: They are most effective, when the Jester is on his way up."},
            { TextKey.TapAnywhereToContinue, "Tap anywhere to continue"}
        };

        private static Dictionary<TextKey, string> _textsDE = new Dictionary<TextKey, string>
        {
            { TextKey.PlayExclamation, "Spielen!"},
            { TextKey.PlayAgainExclamation, "Nochmal Spielen!"},
            { TextKey.Cancel, "Abbruch"},
            { TextKey.UpgradePathKickForceTitle, "Ritterstiefel"},
            { TextKey.UpgradePathKickForceDescription, "Heavier boots mean stronger kicks. The Jester will launch stronger!"},
            { TextKey.UpgradePathShootForceTitle, "Tomaten Festigkeit"},
            { TextKey.UpgradePathShootForceDescription, "Firm tomatoes give the Jester that extra kick he needs."},
            { TextKey.UpgradePathProjectileCountTitle, "Tomato Reserves"},
            { TextKey.UpgradePathProjectileCountDescription, "Increases the amount of tomatoes you can throw in one run."},
            { TextKey.UpgradePathVelocityCapTitle, "Aerodynamic Costume"},
            { TextKey.UpgradePathVelocityCapDescription, "Allows the Jester to reach higher speeds!"},
            { TextKey.ResetProfileDescription, "Are you sure?\n\nThis will reset all your upgrades, ingame-money and records."},
            { TextKey.ResetProfileWarning, "This cannot be undone!"},
            { TextKey.Max, "Max"},
            { TextKey.Level, "Level"},
            { TextKey.DistanceReached, "Distance Reached"},
            { TextKey.NewBest, "New Best!"},
            { TextKey.BestLabel, "Best:"},
            { TextKey.BestDistance, "Best Distance"},
            { TextKey.CurrencyGainTypeDistance, "from distance"},
            { TextKey.CurrencyGainTypePickup, "from pickups"},
            { TextKey.RestoreDefaults, "Restore Defaults"},
            { TextKey.ResetProfile, "Reset Profile"},
            { TextKey.Settings, "Einstellungen"},
            { TextKey.SoundSettings, "Sound Einstellungen"},
            { TextKey.SoundEffects, "Sound Effekte"},
            { TextKey.Music, "Musik"},
            { TextKey.Credits, "Mitwirkende"},
            { TextKey.CreditsCodeHeader, "Code & Spieldesign"},
            { TextKey.CreditsArtHeader, "Art & Animation"},
            { TextKey.CreditsMusicHeader, "Musik"},
            { TextKey.CreditsSoundEffectsHeader, "Sound Effects"},
            { TextKey.HowToPlay, "Anleitung"},
            { TextKey.TutorialStepOne, "Click / Tap anywhere on the screen to kick!"},
            { TextKey.TutorialStepTwo, "This is your Kick Power.\nTry to kick when it is full!"},
            { TextKey.TutorialStepThree, "Click / Tap anywhere on the screen to throw tomatoes at the Jester."},
            { TextKey.TutorialStepThreeTip, "Tip: They are most effective, when the Jester is on his way up."},
            { TextKey.TapAnywhereToContinue, "Tap anywhere to continue"}
        };
    }
}
