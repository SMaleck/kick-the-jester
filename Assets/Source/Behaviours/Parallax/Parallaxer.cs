using Assets.Source.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Behaviours.Parallax
{
    public class Parallaxer : AbstractBehaviour
    {
        [SerializeField] private ParallaxConfig config;
        private List<ParallaxLayer> parallaxLayers;

        private void Start()
        {
            parallaxLayers = new List<ParallaxLayer>();
            foreach (ParallaxLayerConfig layerConfig in config.LayerConfigs)
            {
                parallaxLayers.Add(new ParallaxLayer(this, layerConfig, App.Cache.MainCamera.UCamera.transform));
            }
        }
    }
}
