using Assets.Source.App;
using UniRx;
using UnityEngine;

namespace Assets.Source.Behaviours.MainCamera
{
    public class SkyFade : AbstractBehaviour
    {
        public float maxY = 15;
        public float minY = -5;
        public float maxHeight = 200;


        private void Start()
        {
            App.Cache.RepoRx.JesterStateRepository.HeightProperty
                                 .TakeUntilDestroy(this)
                                 .Subscribe(MoveSkyFade);              
        }


        private void MoveSkyFade(float height)
        {
            float heightPercent = height / maxHeight;           
            float yRange = minY.Difference(maxY);
            
            float nextY = maxY - (heightPercent * yRange);            

            goTransform.localPosition = new Vector3(goTransform.localPosition.x, Mathf.Max(minY, nextY), goTransform.localPosition.z);            
        }
    }
}
