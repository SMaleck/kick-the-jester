using System;
using System.Collections.Generic;
using Assets.Source.Entities.Parallax;
using UnityEngine;

namespace Assets.Source.Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ParallaxConfig", menuName = "Config/ParallaxConfig")]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
