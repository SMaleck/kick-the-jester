using System.Collections.Generic;
using Assets.Source.Entities.Parallax.Config;
using UnityEngine;
using Zenject;

namespace Assets.Source.Entities.Parallax
{
    public class Parallaxer : AbstractMonoEntity
    {
        [SerializeField] private ParallaxConfig config;
        private List<ParallaxLayer> parallaxLayers;

        private Entities.Cameras.MainCamera _camera;

        public override void Initialize()
        {
            parallaxLayers = new List<ParallaxLayer>();
            foreach (ParallaxLayerConfig layerConfig in config.LayerConfigs)
            {
                parallaxLayers.Add(new ParallaxLayer(this, layerConfig, _camera.GoTransform));
            }
        }

        [Inject]
        private void Inject(Entities.Cameras.MainCamera camera)
        {
            _camera = camera;
        }
    }
}
