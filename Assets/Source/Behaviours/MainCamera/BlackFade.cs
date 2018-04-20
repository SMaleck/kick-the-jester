using Assets.Source.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class BlackFade : AbstractBehaviour
    {
        private bool isFading = false;
        private int direction = 1;        
        private float fadeSpeed = 0.65f;

        private SpriteRenderer sprite;

        public void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            App.Cache.gameStateManager.OnGameStateChanged(DecideFade);
        }


        public void Update()
        {
            if (isFading)
            {
                float nextAlpha = sprite.color.a + ((fadeSpeed * direction) * Time.deltaTime);
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Clamp(nextAlpha, 0, 1));

                // Turn off Fading, if we reached one of the maximums
                isFading = sprite.color.a <= 0 || sprite.color.a >= 1;
            }
        }


        private void DecideFade(GameStateMachine.GameState state)
        {
            if(state == GameStateMachine.GameState.Switching)
            {
                FadeOut();
            }
        }


        public void FadeIn()
        {
            direction = -1;
            isFading = true;
        }


        public void FadeOut()
        {
            direction = 1;
            isFading = true;
        }
    }
}
