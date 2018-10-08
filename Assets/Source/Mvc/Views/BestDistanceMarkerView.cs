using Assets.Source.Util;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Source.Mvc.Views
{
    public class BestDistanceMarkerView : AbstractView
    {
        [SerializeField] private TMP_Text _bestDistanceText;

        // ToDo [CONFIG] Move to config SO
        private const float MoveThresholdDistanceMeters = 5;
        private const float MoveSeconds = 0.7f;
        private Vector3 _origin;

        public float BestDistance { set { UpdateBestDistance(value); } }


        public override void Setup()
        {
            _origin = gameObject.transform.position;
        }

        
        private void UpdateBestDistance(float distance)
        {
            _bestDistanceText.text = distance.ToMetersString();

            if (distance.ToMeters() < MoveThresholdDistanceMeters)
            {
                return;
            }

            gameObject.transform.DOMoveX(_origin.x + distance, MoveSeconds)
                .SetEase(Ease.OutCubic);
        }
    }
}
