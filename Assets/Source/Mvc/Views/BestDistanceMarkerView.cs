using Assets.Source.Entities.Jester;
using Assets.Source.Features.Statistics;
using Assets.Source.Services.Localization;
using Assets.Source.Util;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public class BestDistanceMarkerView : AbstractView
    {
        [SerializeField] private TMP_Text _bestDistanceLabelText;
        [SerializeField] private TMP_Text _bestDistanceText;

        private const float MoveThresholdDistanceMeters = 5;
        private const float MoveSeconds = 0.7f;

        private IStatisticsModel _statisticsModel;
        private Vector3 _selfOrigin;
        private Vector3 _jesterOrigin;

        [Inject]
        public void Inject(
            IStatisticsModel statisticsModel,
            JesterEntity jesterEntity)
        {
            _statisticsModel = statisticsModel;
            _selfOrigin = gameObject.transform.position;
            _jesterOrigin = jesterEntity.Position;
        }

        public override void Setup()
        {
            _statisticsModel.BestDistanceUnits
                .Subscribe(UpdateBestDistance)
                .AddTo(Disposer);
        }

        private void UpdateBestDistance(float distance)
        {
            UpdateTexts();
            SlideToBestDistance(distance);
        }

        private void UpdateBestDistanceInstant(float distance)
        {
            UpdateTexts();
            SlideToBestDistance(distance, true);
        }

        private void UpdateTexts()
        {
            var distance = _statisticsModel.BestDistanceUnits.Value;
            _bestDistanceText.text = TextService.MetersAmount(distance);
            _bestDistanceLabelText.text = TextService.BestDistance();
        }

        private void SlideToBestDistance(float distance, bool instant = false)
        {
            var isAboveThreshold = IsAboveThreshold(distance);
            SetActive(isAboveThreshold);

            if (!isAboveThreshold)
            {
                return;
            }

            var newPos = new Vector3(
                _jesterOrigin.x + distance,
                _selfOrigin.y,
                _selfOrigin.z);

            if (instant)
            {
                gameObject.transform.position = newPos;
                return;
            }

            gameObject.transform
                .DOMove(newPos, MoveSeconds)
                .SetEase(Ease.OutCubic);
        }

        private bool IsAboveThreshold(float distance)
        {
            return distance.ToMeters() > MoveThresholdDistanceMeters;
        }
    }
}
