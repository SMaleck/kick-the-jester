using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Entities.Jester.Config
{
    [CreateAssetMenu(fileName = "JesterSoundEffectsConfig", menuName = Constants.PROJECT_MENU_ROOT + "/JesterSoundEffectsConfig")]
    public class JesterSoundEffectsConfig : ScriptableObject
    {        
        public AudioClip GroundHit; 
    }
}
