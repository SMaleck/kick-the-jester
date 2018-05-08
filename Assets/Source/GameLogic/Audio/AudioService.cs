using UnityEngine;

namespace Assets.Source.GameLogic.Audio
{
    public enum SoundEffects { Kick, GroundHit, Pickup, BoostHit};

    public class AudioService : MonoBehaviour
    {
        [SerializeField] AudioChannel BGMChannel;
        [SerializeField] AudioChannel SFXChannel;
        

        /* -------------------------------------- BGM CHANNEL -------------------------------------- */
        public void PlayBGM(AudioClip clip)
        {
            BGMChannel.PlayClip(clip);
        }

        public void PlayLoopingBGM(AudioClip clip)
        {
            BGMChannel.PlayClip(clip, true, false);
        }

        public void ToggleBGMMuted()
        {
            BGMChannel.ToggleMuted();
        }


        /* -------------------------------------- SFX CHANNEL -------------------------------------- */
        public void PlaySFX(AudioClip clip)
        {
            SFXChannel.PlayClip(clip);
        }

        public void PlayRandomizedSFX(AudioClip clip)
        {
            SFXChannel.PlayClip(clip, false, true);
        }

        public void ToggleSFXMuted()
        {
            SFXChannel.ToggleMuted();
        }        
    }
}
