using UnityEngine;

namespace Assets.Source.UI.Elements
{
    public class Pulse : MonoBehaviour
    {        
        [SerializeField] float pulseTimeSeconds = 0.5f;        
        [SerializeField] float maxScaleFactor = 1.1f;

        private LTDescr pulseTween;

        private void Awake()
        {
            pulseTween = LeanTween.scale(gameObject.GetComponent<RectTransform>(), gameObject.GetComponent<RectTransform>().localScale * maxScaleFactor, pulseTimeSeconds)
                                  .setEaseInOutCubic()
                                  .setLoopPingPong();            
        }


        public void Stop()
        {
            pulseTween.setLoopCount(0);
        }
    }
}
