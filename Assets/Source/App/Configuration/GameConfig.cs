using System;
using UnityEngine;

namespace Assets.Source.App.Configuration
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "KTJ/Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public CameraConfig CameraConfig = new CameraConfig();
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
}
