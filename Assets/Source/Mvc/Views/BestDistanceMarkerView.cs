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
        private Vector3 _selfOrigin;

        public Vector3 JesterOrigin;        


        public override void Setup()
        {
            _selfOrigin = gameObject.transform.position;
        }


        public void UpdateBestDistance(float distance)
        {
            _bestDistanceText.text = distance.ToMetersString();
            SlideToBestDistance(distance);
        }

        public void UpdateBestDistanceInstant(float distance)
        {
            _bestDistanceText.text = distance.ToMetersString();
            SlideToBestDistance(distance, true);
        }


        private void SlideToBestDistance(float distance, bool instant = false)
        {
            var isAboveThreshold = IsAboveThreshold(distance);
            SetActive(isAboveThreshold);

            if (!isAboveThreshold)
            {                
                return;
            }

            var newPosX = JesterOrigin.x + distance;
            if (instant)
            {
                gameObject.transform.position = new Vector3(newPosX, _selfOrigin.y, _selfOrigin.z);
                return;
            }

            gameObject.transform.DOMoveX(newPosX, MoveSeconds)
                .SetEase(Ease.OutCubic);
        }


        private bool IsAboveThreshold(float distance)
        {
            return distance.ToMeters() > MoveThresholdDistanceMeters;
        }
    }
}
