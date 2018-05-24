using System;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{
    [Serializable]
    public class ParallaxLayerConfig
    {
        public string SortingLayer = "ParallaxBackground";
        public int SortingOrder = 0;

        public bool UseSpeed;
        public bool Repeat;
        
        // <0: Super Speed!
        // 0 : Same speed as the Camera, i.e. normal speed
        // 1 : Synched with Camera, i.e. will never move out
        // >1: Move forward :D
        [Range(0.0f, 1.0f)]
        public float Speed;
        public Sprite Sprite;

        public Vector3 LocalPosition = Vector3.zero;
        public Vector3 Scale = Vector3.one;
    }
}
