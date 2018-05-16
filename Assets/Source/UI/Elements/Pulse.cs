using UnityEngine;

namespace Assets.Source.UI.Elements
{
    public class Pulse : MonoBehaviour
    {        
        [SerializeField] float pulseTimeSeconds = 0.5f;        
        [SerializeField] float maxScaleFactor = 1.1f;

        private void Awake()
        {
            LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale * maxScaleFactor, pulseTimeSeconds)
                     .setEaseInOutCubic()
                     .setLoopPingPong();            
        }
    }
}
