using UnityEngine;

namespace Assets.Source.App.Configuration
{
    // ToDo Not used, fix that
    [CreateAssetMenu(fileName = "DefaultSettingsConfig", menuName = "Config/DefaultSettingsConfig")]
    public class DefaultSettingsConfig : ScriptableObject
    {
        public float MusicVolume;
        public float EffectVolume;
    }
}
