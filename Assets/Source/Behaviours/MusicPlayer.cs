﻿using Assets.Source.App;
using UnityEngine;

namespace Assets.Source.Behaviours
{
    public class MusicPlayer : AbstractBehaviour
    {
        [SerializeField] AudioClip musicClip;

        private void Start()
        {
            App.Cache.Kernel.AudioService.PlayLoopingBGM(musicClip);
        }
    }
}
