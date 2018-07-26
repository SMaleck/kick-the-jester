using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Source.UI.Elements
{
    public class FloatingValue : MonoBehaviour
    {
        private bool isScaling = false;
        private bool isFading = false;

        public bool IsFloating { get { return isScaling || isFading; } }

        public TMP_Text Text;
        public float FloatTimeSeconds = 0.8f;
        public float StartFontSize = 32;
        public float EndFontSize = 64;

        private float fadeDelay { get { return FloatTimeSeconds * 0.9f; } }
        private float fadeTimeSeconds = 0.5f;


        public void StartFloating(string textValue)
        {
            if(IsFloating) { return; }

            // Reset
            Text.text = textValue;
            Text.fontSize = StartFontSize;
            Text.alpha = 1;

            isScaling = true;
            LeanTween.value(this.gameObject, (float value) => { Text.fontSize = value; }, StartFontSize, EndFontSize, FloatTimeSeconds)
                     .setOnComplete(_ => { isScaling = false; });

            // Fade out text after it has floated for a while
            isFading = true;
            Observable.Timer(TimeSpan.FromSeconds(fadeDelay))
                      .Subscribe(_ => 
                      {
                          LeanTween.value(this.gameObject, (float value) => { Text.alpha = value; }, 1, 0, fadeTimeSeconds)
                                   .setEaseInOutCubic()
                                   .setOnComplete(e => { isFading = false; });
                      })
                      .AddTo(this);
            
        }
    }
}
