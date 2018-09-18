using DG.Tweening;
using UnityEngine;

namespace Assets.Source.Util.UI
{    
    public class Pulse : MonoBehaviour
    {        
        [SerializeField] float pulseTimeSeconds = 0.5f;        
        [SerializeField] float maxScaleFactor = 1.1f;

        private RectTransform target;
        private Tweener pulseTween;

        private void Awake()
        {
            target = GetComponent<RectTransform>();

            if(target == null) { return; }

            pulseTween = target?.DOScale(maxScaleFactor, pulseTimeSeconds)
                                .SetEase(Ease.InOutCubic)
                                .SetLoops(-1, LoopType.Yoyo);         
        }


        public void Stop()
        {
            pulseTween.Pause();
        }
    }
}
