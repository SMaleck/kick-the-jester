using System;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{
    [Serializable]
    public class ParallaxLayerConfig
    {
        [SerializeField] public string SortingLayer = "ParallaxBackground";
        [SerializeField] public int SortingOrder = 0;

        [SerializeField] public bool UseSpeed = true;
        [SerializeField] public bool Repeat = true;
        
        // <0: Super Speed!
        // 0 : Same speed as the Camera, i.e. normal speed
        // 1 : Synched with Camera, i.e. will never move out
        // >1: Move forward :D
        [Range(0.0f, 1.0f)]
        [SerializeField] public float Speed = 0.5f;
        [SerializeField] public Sprite Sprite;

        [SerializeField] public Vector3 Position = Vector3.zero;
        [SerializeField] public Vector3 Scale = Vector3.one;
    }
}
