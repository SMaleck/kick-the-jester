using System;
using UniRx;
using UnityEngine;

namespace Assets.Source.App.Storage
{
    public class UserSettingsService : AbstractPersistentDataService
    {
        private const string MUTE_BGM_KEY = "MuteBGM";
        public BoolReactiveProperty MuteBGMProperty = new BoolReactiveProperty(false);
        public bool MuteBGM
        {
            get { return MuteBGMProperty.Value; }
            set
            {
                MuteBGMProperty.Value = value;
                PlayerPrefs.SetInt(MUTE_BGM_KEY, value ? 1 : 0);
            }
        }

        private const string MUTE_SFX_KEY = "MuteSFX";
        public BoolReactiveProperty MuteSFXProperty = new BoolReactiveProperty(false);
        public bool MuteSFX
        {
            get { return MuteSFXProperty.Value; }
            set
            {
                MuteSFXProperty.Value = value;
                PlayerPrefs.SetInt(MUTE_SFX_KEY, value ? 1 : 0);
            }
        }


        public UserSettingsService()
            : base()
        {
            if (PlayerPrefs.HasKey(MUTE_BGM_KEY))
            {
                MuteBGM = Convert.ToBoolean(PlayerPrefs.GetInt(MUTE_BGM_KEY));
            }

            if (PlayerPrefs.HasKey(MUTE_SFX_KEY))
            {
                MuteSFX = Convert.ToBoolean(PlayerPrefs.GetInt(MUTE_SFX_KEY));
            }          
        }


        public override void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
