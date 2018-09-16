﻿using System;
using UnityEngine;

namespace Assets.Source.Util.UI
{
    public enum SlideEvent { Open, Close };
    public enum SlideDirection { Top, Bottom, Left, Right };

    [Serializable]
    public class PanelSliderConfig
    {                        
        public SlideDirection slideInFrom = SlideDirection.Top;
        public bool useSlideTransition = false;
        public float slideTimeSeconds = 0.5f;        
        public bool useBounce = false;

        [Header("Transition Sounds")]
        public AudioClip sfxShow;
        public AudioClip sfxHide;
    }
}