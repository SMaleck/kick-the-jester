using Assets.Source.Util;
using TMPro;
using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class BestDistanceMarkerView : AbstractView
    {
        [SerializeField] private TMP_Text _bestDistanceText;

        public FloatReactiveProperty BestDistance = new FloatReactiveProperty();
        

        public override void Setup()
        {
            BestDistance
                .Subscribe(OnBestDistanceChanged)
                .AddTo(this);
        }

        // ToDo Move to best distance
        private void OnBestDistanceChanged(float distance)
        {
            _bestDistanceText.text = distance.ToMetersString();
        }
    }
}
