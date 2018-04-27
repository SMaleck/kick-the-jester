using UnityEngine;

namespace Assets.Source.Behaviours.Jester
{
    public class JesterSounds : AbstractBehaviour
    {
        [SerializeField]
        private AudioClip acGroundHit;
        private AudioSource GroundHit;

        private void Awake()
        {
            GroundHit = gameObject.AddComponent<AudioSource>();
            GroundHit.clip = acGroundHit;


            App.Cache.jester.Collisions.OnGround(() => PlayRandomizedSound(GroundHit));            
        }
        

        private void PlayRandomizedSound(AudioSource source)
        {
            source.pitch = UnityEngine.Random.Range(0.65f, 1.2f);
            source.Play();
        }
    }
}
