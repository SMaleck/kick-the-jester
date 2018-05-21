using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera.Components
{
    public class ScreenFader : AbstractComponent<CameraBase>
    {
        private bool isFading = false;
        private int direction = 1;
        private float fadeSpeed = 2.3f;

        private SpriteRenderer sprite;

        // FADE COMPLETION EVENTS
        private event NotifyEventHandler _OnFadeInComplete = delegate { };
        private event NotifyEventHandler _OnFadeOutComplete = delegate { };


        public ScreenFader(CameraBase owner, SpriteRenderer sprite)
            : base(owner, false)
        {
            // Ensure the Sprite is at full ALPHA
            this.sprite = sprite;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }


        protected override void Update()
        {
            if (isFading)
            {
                float nextAlpha = sprite.color.a + ((fadeSpeed * direction) * Time.deltaTime);
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Clamp(nextAlpha, 0, 1));

                // Turn off Fading, if we reached one of the maximums
                isFading = sprite.color.a > 0 && sprite.color.a < 1;

                // Call Handlers, if this was the last step of fading
                CheckFadeCompletionState();
            }
        }


        public void FadeIn(NotifyEventHandler callback)
        {
            direction = -1;
            isFading = true;

            if (callback != null)
            {
                _OnFadeInComplete += callback;
            }
        }


        public void FadeOut(NotifyEventHandler callback)
        {
            direction = 1;
            isFading = true;

            if (callback != null)
            {
                _OnFadeOutComplete += callback;
            }
        }


        // Fires the appropriate fading event and resets handlers
        private void CheckFadeCompletionState()
        {
            if (!isFading && direction <= -1)
            {
                _OnFadeInComplete();
                //_OnFadeInComplete = delegate { };
            }

            if (!isFading && direction >= 1)
            {
                _OnFadeOutComplete();
                //_OnFadeOutComplete = delegate { };
            }
        }
    }
}
