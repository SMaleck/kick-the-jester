using UnityEngine;

namespace Assets.Source.App.Configuration
{
    [CreateAssetMenu(fileName = "DefaultSettingsConfig", menuName = "Config/DefaultSettingsConfig")]
    public class DefaultSettingsConfig : ScriptableObject
    {
        public float MusicVolume;
        public float EffectVolume;
    }
}
