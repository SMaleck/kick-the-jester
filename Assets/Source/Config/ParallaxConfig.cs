using Assets.Source.Behaviours.Parallax;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Config
{
    [Serializable]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
