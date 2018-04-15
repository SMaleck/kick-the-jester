using Assets.Source.Structs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.Entities
{
    public class Parallaxer : BaseEntity
    {
        private const string PRLX_LAYERNAME = "ParallaxBackground";
        private ParallaxLayer[] BGLayers;
        

        public void Awake()
        {            
            BGLayers = gameObject.GetComponentsInChildren<ParallaxLayer>();
            DistributeSortingLayers();
            SetupCopies();

            App.Cache.rxState.AttachForFlightStats(OnFlightStatsChanged);            
        }


        private void DistributeSortingLayers()
        {
            SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();

            int i = 0;

            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.sortingLayerName = PRLX_LAYERNAME;
                renderer.sortingOrder = i;
                i--;
            }
        }


        private void SetupCopies()
        {
            List<ParallaxLayer> copies = new List<ParallaxLayer>();

            foreach (ParallaxLayer layer in BGLayers)
            {
                if (layer.Loop)
                {
                    layer.CreateOffsettedCopy();
                }
            }
        }


        private void OnFlightStatsChanged(FlightStats stats)
        {
            foreach(ParallaxLayer layer in BGLayers)
            {
                layer.SetVelocity(new Vector2(stats.Velocity.x, 0));
            }
        }
    }
}
