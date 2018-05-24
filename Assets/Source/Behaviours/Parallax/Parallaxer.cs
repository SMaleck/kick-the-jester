using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{
    public class Parallaxer : AbstractBehaviour
    {
        [SerializeField] public List<ParallaxLayerConfig> LayerConfigs;
        private List<ParallaxLayer> parallaxLayers;

        private void Start()
        {
            parallaxLayers = new List<ParallaxLayer>();
            foreach (ParallaxLayerConfig config in LayerConfigs)
            {
                parallaxLayers.Add(new ParallaxLayer(this, config, App.Cache.MainCamera.UCamera.transform));
            }
        }
    }
}
