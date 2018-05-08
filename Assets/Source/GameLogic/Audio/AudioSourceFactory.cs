using UnityEngine;

namespace Assets.Source.GameLogic.Audio
{
    public class AudioSourceFactory : MonoBehaviour
    {
        public AudioSource Create()
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            return audioSource;
        }
    }
}
