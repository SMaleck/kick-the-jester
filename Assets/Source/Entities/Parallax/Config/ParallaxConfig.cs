using Assets.Source.App;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities.Parallax.Config
{
    [Serializable]
    [CreateAssetMenu(fileName = "ParallaxConfig", menuName = Constants.PROJECT_MENU_ROOT + "/ParallaxConfig")]
    public class ParallaxConfig : ScriptableObject
    {
        public List<ParallaxLayerConfig> LayerConfigs;
    }
}
