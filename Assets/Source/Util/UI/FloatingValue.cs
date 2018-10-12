using System;
using TMPro;
using UniRx;
using UnityEngine;
using DG.Tweening;

namespace Assets.Source.Util.UI
{    
    public class FloatingValue : MonoBehaviour
    {
        private bool isScaling = false;
        private bool isFading = false;

        public bool IsFloating { get; private set; }

        public TMP_Text Text;
        public float FloatTimeSeconds = 0.8f;
        public float StartFontSize = 32;
        public float EndFontSize = 64;

        private float fadeDelay { get { return FloatTimeSeconds * 0.9f; } }
        private float fadeTimeSeconds = 0.5f;


        public void StartFloating(string textValue)
        {
            if(IsFloating) { return; }

            IsFloating = true;

            // Reset
            Text.text = textValue;            
            Text.alpha = 1;

            DOTween.To(size => Text.fontSize = size, StartFontSize, EndFontSize, FloatTimeSeconds);

            Observable.Timer(TimeSpan.FromSeconds(fadeDelay))
                .Subscribe(_ => 
                {
                    DOTween.To(size => Text.alpha = size, 1, 0, fadeTimeSeconds)
                    .OnComplete(() => { IsFloating = false; });
                })
                .AddTo(this);            
        }
    }
}
