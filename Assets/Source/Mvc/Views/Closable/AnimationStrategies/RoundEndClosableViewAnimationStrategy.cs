using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Source.Mvc.Views.Closable.AnimationStrategies
{
    public class RoundEndClosableViewAnimationStrategy : MonoBehaviour, IClosableViewAnimationStrategy
    {
        [SerializeField] private GameObject _closableParent;

        [Header("Main Panel")]
        [SerializeField] private RectTransform _panelParent;
        [SerializeField] private float _panelStartX;

        [Header("Buttons")]
        [SerializeField] private RectTransform _buttonsParent;
        [SerializeField] private float _buttonsStartX;

        [Header("Settings")]
        [SerializeField] private float _durationSeconds;

        public void Initialize(bool startClosed)
        {
            _closableParent.gameObject.SetActive(!startClosed);
        }

        public void Open(Action onComplete)
        {
            _closableParent.gameObject.SetActive(true);
            
            DOTween.Sequence()
                .Append(_panelParent.DOAnchorPosX(_panelStartX, _durationSeconds).From())
                .Append(_buttonsParent.DOAnchorPosX(_buttonsStartX, _durationSeconds).From())
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Close(Action onComplete)
        {
            // This view is never closed manually
            onComplete?.Invoke();
        }
    }
}
