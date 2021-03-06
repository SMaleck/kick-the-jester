﻿using System;
using UnityEngine;

namespace Assets.Source.Util.UI
{
    public enum SlideEvent { Open, Close };
    public enum SlideDirection { Instant, Top, Bottom, Left, Right };

    [Serializable]
    public class PanelSliderConfig
    {
        public SlideDirection slideInFrom = SlideDirection.Instant;
        public bool useBounce = false;
        public bool SoundEffectsEnabled = true;

        [Tooltip("Enables sliding Sound effects when INSTANT Transition mode is selected")]
        public bool SoundEffectsEnabledOnInstant = false;

        [Range(0f, 5f)]
        public float slideTimeSeconds = 0.5f;
    }
}
