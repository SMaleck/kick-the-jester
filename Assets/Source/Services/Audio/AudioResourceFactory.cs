using Assets.Source.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Source.Services.Audio
{
    public class AudioResourceFactory : IFactory<PoolableAudioSource>
    {
        private readonly GameObject _audioSourceContainer;

        public AudioResourceFactory()
        {
            _audioSourceContainer = new GameObject();
            _audioSourceContainer.name = "AudioSourceContainer";
            Object.DontDestroyOnLoad(_audioSourceContainer);
        }

        public PoolableAudioSource CreateResource()
        {
            AudioSource audioSource = _audioSourceContainer.AddComponent<AudioSource>();
            return new PoolableAudioSource(audioSource);
        }
    }
}
