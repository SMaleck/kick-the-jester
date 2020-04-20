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

        public static string GetLanguageText(Language languageKey)
        {
            return _textsLANGUAGE[languageKey];
        }

        private static Dictionary<Language, string> _textsLANGUAGE = new Dictionary<Language, string>
        {
            { Language.English, "English"},
            { Language.German, "Deutsch"}
        };

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
            { TextKey.CurrencyGainTypeDistance, "Distance Bonus"},
            { TextKey.CurrencyGainTypePickup, "Pickups"},
            { TextKey.CurrencyGainTypeShortDistanceBonus, "Participation Bonus"},
            { TextKey.CurrencyGainTypeMaxHeightBonus, "Rising Star Bonus"},
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
            { TextKey.TapAnywhereToContinue, "Tap anywhere to continue"},
            { TextKey.Pause, "Pause"},
            { TextKey.Language, "Language"},
            { TextKey.Restart, "Restart"},
            { TextKey.Continue, "Continue"},
            { TextKey.Upgrades, "Upgrades"},
            { TextKey.Achievements, "Achievements"},
            { TextKey.AchievementNameReachedMoon, "AchievementNameReachedMoon" },
            { TextKey.AchievementNameBestDistance1, "AchievementNameBestDistance1" },
            { TextKey.AchievementNameBestDistance2, "AchievementNameBestDistance2" },
            { TextKey.AchievementNameBestDistance3, "AchievementNameBestDistance3" },
            { TextKey.AchievementNameTotalDistance1, "AchievementNameTotalDistance1" },
            { TextKey.AchievementNameTotalDistance2, "AchievementNameTotalDistance2" },
            { TextKey.AchievementNameTotalDistance3, "AchievementNameTotalDistance3" },
            { TextKey.AchievementNameBestHeight1, "AchievementNameBestHeight1" },
            { TextKey.AchievementNameBestHeight2, "AchievementNameBestHeight2" },
            { TextKey.AchievementNameBestHeight3, "AchievementNameBestHeight3" },
            { TextKey.AchievementNameRoundsPlayed1, "AchievementNameRoundsPlayed1" },
            { TextKey.AchievementNameRoundsPlayed2, "AchievementNameRoundsPlayed2" },
            { TextKey.AchievementNameRoundsPlayed3, "AchievementNameRoundsPlayed" },
            { TextKey.AchievementRequirementReachMoon, "AchievementRequirementReachMoon" },
            { TextKey.AchievementRequirementBestDistance, "AchievementRequirementBestDistance" },
            { TextKey.AchievementRequirementTotalDistance, "AchievementRequirementTotalDistance" },
            { TextKey.AchievementRequirementBestHeight, "AchievementRequirementBestHeight" },
            { TextKey.AchievementRequirementRoundsPlayed, "AchievementRequirementRoundsPlayed" }
        };

        private static Dictionary<TextKey, string> _textsDE = new Dictionary<TextKey, string>
        {
            { TextKey.PlayExclamation, "Spielen!"},
            { TextKey.PlayAgainExclamation, "Nochmal Spielen!"},
            { TextKey.Cancel, "Abbruch"},
            { TextKey.UpgradePathKickForceTitle, "Ritterstiefel"},
            { TextKey.UpgradePathKickForceDescription, "Stärkere Tritte durch schwerere Stiefel. Der Hofnarr wird stärker starten!"},
            { TextKey.UpgradePathShootForceTitle, "Tomatenfestigkeit"},
            { TextKey.UpgradePathShootForceDescription, "Feste Tomaten geben dem Hofnarr mehr Schwung."},
            { TextKey.UpgradePathProjectileCountTitle, "Tomatenreserve"},
            { TextKey.UpgradePathProjectileCountDescription, "Erhöht die Anzahl an Tomaten, die du in einem Durchlauf werfen kannst."},
            { TextKey.UpgradePathVelocityCapTitle, "Aerodynamisches Kostüm"},
            { TextKey.UpgradePathVelocityCapDescription, "Damit kann der Hofnarr höhere Geschwindigkeiten erzielen!"},
            { TextKey.ResetProfileDescription, "Bist du sicher?\n\nDas wird alle deine Verbessrungen, Goldmünzen und Rekorde zurücksetzen."},
            { TextKey.ResetProfileWarning, "Dies kann nicht rückgängig gemacht werden"},
            { TextKey.Max, "Max"},
            { TextKey.Level, "Level"},
            { TextKey.DistanceReached, "Erreichte Distanz"},
            { TextKey.NewBest, "Neuer Rekord!"},
            { TextKey.BestLabel, "Rekord:"},
            { TextKey.BestDistance, "Beste Distanz"},
            { TextKey.CurrencyGainTypeDistance, "Distanzbonus"},
            { TextKey.CurrencyGainTypePickup, "Gegenstände"},
            { TextKey.CurrencyGainTypeShortDistanceBonus, "Trostpreis"},
            { TextKey.CurrencyGainTypeMaxHeightBonus, "Sternschnuppenbonus"},
            { TextKey.RestoreDefaults, "Standard wiederherstellen"},
            { TextKey.ResetProfile, "Profil zurücksetzen"},
            { TextKey.Settings, "Einstellungen"},
            { TextKey.SoundSettings, "Sound Einstellungen"},
            { TextKey.SoundEffects, "Sound Effekte"},
            { TextKey.Music, "Musik"},
            { TextKey.Credits, "Mitwirkende"},
            { TextKey.CreditsCodeHeader, "Code & Spieldesign"},
            { TextKey.CreditsArtHeader, "Art & Animation"},
            { TextKey.CreditsMusicHeader, "Musik"},
            { TextKey.CreditsSoundEffectsHeader, "Sound Effekte"},
            { TextKey.HowToPlay, "Anleitung"},
            { TextKey.TutorialStepOne, "Klicke / Tippe auf eine beliebige Stelle auf dem Bildschirm, um zu treten!"},
            { TextKey.TutorialStepTwo, "Dies ist deine Tretkraft.\nVersuche zu treten, wenn sie voll ist!"},
            { TextKey.TutorialStepThree, "Klicke / Tippe auf eine beliebige Stelle auf dem Bildschirm, um Tomaten auf den Hofnarr zu werfen."},
            { TextKey.TutorialStepThreeTip, "Tip: Sie sind am effektivsten, wenn der Hofnarr auf dem Weg nach oben ist."},
            { TextKey.TapAnywhereToContinue, "Klicke / Tippe auf den Bildschirm um fortzufahren!"},
            { TextKey.Pause, "Pause"},
            { TextKey.Language, "Sprache"},
            { TextKey.Restart, "Neustarten"},
            { TextKey.Continue, "Weiter"},
            { TextKey.Upgrades, "Verbesserungen"},
            { TextKey.Achievements, "Erfolge"},
            { TextKey.AchievementNameReachedMoon, "AchievementNameReachedMoon" },
            { TextKey.AchievementNameBestDistance1, "AchievementNameBestDistance1" },
            { TextKey.AchievementNameBestDistance2, "AchievementNameBestDistance2" },
            { TextKey.AchievementNameBestDistance3, "AchievementNameBestDistance3" },
            { TextKey.AchievementNameTotalDistance1, "AchievementNameTotalDistance1" },
            { TextKey.AchievementNameTotalDistance2, "AchievementNameTotalDistance2" },
            { TextKey.AchievementNameTotalDistance3, "AchievementNameTotalDistance3" },
            { TextKey.AchievementNameBestHeight1, "AchievementNameBestHeight1" },
            { TextKey.AchievementNameBestHeight2, "AchievementNameBestHeight2" },
            { TextKey.AchievementNameBestHeight3, "AchievementNameBestHeight3" },
            { TextKey.AchievementNameRoundsPlayed1, "AchievementNameRoundsPlayed1" },
            { TextKey.AchievementNameRoundsPlayed2, "AchievementNameRoundsPlayed2" },
            { TextKey.AchievementNameRoundsPlayed3, "AchievementNameRoundsPlayed" },
            { TextKey.AchievementRequirementReachMoon, "AchievementRequirementReachMoon" },
            { TextKey.AchievementRequirementBestDistance, "AchievementRequirementBestDistance" },
            { TextKey.AchievementRequirementTotalDistance, "AchievementRequirementTotalDistance" },
            { TextKey.AchievementRequirementBestHeight, "AchievementRequirementBestHeight" },
            { TextKey.AchievementRequirementRoundsPlayed, "AchievementRequirementRoundsPlayed" }
        };
    }
}
