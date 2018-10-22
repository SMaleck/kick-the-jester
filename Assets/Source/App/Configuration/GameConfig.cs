using System;
using UnityEngine;

namespace Assets.Source.App.Configuration
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "KTJ/Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public DefaultSettingsConfig DefaultSettingsConfig = new DefaultSettingsConfig();
        public CameraConfig CameraConfig = new CameraConfig();
        public AudioConfig AudioConfig = new AudioConfig();
        public BalancingConfig BalancingConfig = new BalancingConfig();
        public BootConfig BootConfig = new BootConfig();
    }

    [Serializable]
    public class DefaultSettingsConfig
    {
        [Range(0f, 1f)]
        public float MusicVolume = 1.0f;

        [Range(0f, 1f)]
        public float EffectVolume = 0.8f;
    }

    [Serializable]
    public class CameraConfig
    {        
        public float OffsetX = 3.5f;
        public float OvertakeOffsetX = 5.5f;
        public float OvertakeSeconds = 0.8f;

        [Range(0f, 1f)]
        public float RelativeVelocityThresholdForShake = 0.5f;

        [Range(0f, 1f)]
        public float ShakeSeconds = 0.1f;
    }

    [Serializable]
    public class AudioConfig
    {
        public float MinPitch = 0.65f;
        public float MaxPitch = 1.5f;
    }

    [Serializable]
    public class BalancingConfig
    {
        [Range(0.01f, 5f)]
        public float MeterToGoldFactor = 0.5f;        
    }

    [Serializable]
    public class BootConfig
    {
        public Vector3 ForceDirection = new Vector3(1, 1, 1);

        [Range(0.01f, 2f)]
        public float ForceFactorChangeSeconds = 0.9f;

        [Range(0f, 5f)]
        public float MinForceFactor = 0.1f;

        [Range(0f, 5f)]
        public float MaxForceFactor = 1f;
    }
}
