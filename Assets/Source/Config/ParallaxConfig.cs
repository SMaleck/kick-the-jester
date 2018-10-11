﻿using System;
using System.Collections.Generic;
using Assets.Source.Entities.Parallax;
using UnityEngine;

// ToDo Move Namespace
namespace Assets.Source.Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ParallaxConfig", menuName = "KTJ/Config/ParallaxConfig")]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
