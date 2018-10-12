using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Parallax.Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ParallaxConfig", menuName = "KTJ/Config/ParallaxConfig")]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
