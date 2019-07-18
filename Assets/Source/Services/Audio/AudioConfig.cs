using Assets.Source.App;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source.Services.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = Constants.PROJECT_MENU_ROOT + "/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [Serializable]
        private class AudioClipItem
        {
            [SerializeField] private AudioClipType _audioClipType;
            public AudioClipType AudioClipType => _audioClipType;

            [SerializeField] private AudioClip _audioClip;
            public AudioClip AudioClip => _audioClip;
        }

        [SerializeField] private AudioSource _audioSourcePrefab;
        public AudioSource AudioSourcePrefab => _audioSourcePrefab;

        [SerializeField] private float _minPitch;
        public float MinPitch => _minPitch;

        [SerializeField] private float _maxPitch;
        public float MaxPitch => _maxPitch;

        [SerializeField] private List<AudioClipItem> _audioClipItems;

        public AudioClip GetAudioClip(AudioClipType audioClipType)
        {
            return _audioClipItems
                .First(item => item.AudioClipType == audioClipType)
                .AudioClip;
        }
    }
}
