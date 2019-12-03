using Assets.Source.App;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Parallax.Config
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(ParallaxConfig), menuName = Constants.UMenuRoot + nameof(ParallaxConfig))]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
