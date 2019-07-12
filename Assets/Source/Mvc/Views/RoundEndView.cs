using Assets.Source.Util;
using Assets.Source.Util.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class RoundEndView : ClosableView
    {
        [Header("Distance Results")]
        [SerializeField] Text _distanceText;
        [SerializeField] Text _bestDistanceText;
        [SerializeField] GameObject _newBestLabel;

        [Header("Currency Results")]
        [SerializeField] RectTransform _currencyContainer;
        [SerializeField] TMP_Text _currencyText;
        [SerializeField] GameObject _pfCurrencyItem;

        [Header("Buttons")]
        [SerializeField] Button _retryButton;
        [SerializeField] Button _shopButton;

        private readonly ReactiveCommand _onRetryClicked = new ReactiveCommand();
        public IObservable<Unit> OnRetryClicked => _onRetryClicked;

        private readonly ReactiveCommand _onUpgradesClicked = new ReactiveCommand();
        public IObservable<Unit> OnUpgradesClicked => _onUpgradesClicked;

        public float Distance { set { _distanceText.text = value.ToMetersString(); } }
        public float BestDistance { set { _bestDistanceText.text = value.ToMetersString(); } }
        public bool IsNewBestDistance { set { _newBestLabel.SetActive(value); } }

        private const float CurrencyCounterSeconds = 1f;

        public override void Setup()
        {
            base.Setup();

            _onRetryClicked.AddTo(Disposer);
            _onRetryClicked.BindTo(_retryButton).AddTo(Disposer);

            _onUpgradesClicked.AddTo(Disposer);
            _onUpgradesClicked.BindTo(_shopButton).AddTo(Disposer);

            _currencyText.text = string.Empty;
        }


        public void ShowCurrencyResults(IDictionary<string, int> results, int currencyAmountAtStart)
        {
            List<CurrencyItem> currencyItems = new List<CurrencyItem>();

            Rect pfRect = _pfCurrencyItem.GetComponent<RectTransform>().rect;

            // Create currency Items
            int index = 0;

            foreach (string key in results.Keys)
            {
                GameObject go = GameObject.Instantiate(_pfCurrencyItem, _currencyContainer, false);
                go.SetActive(true);

                currencyItems.Add(go.GetComponent<CurrencyItem>());

                var ci = currencyItems.Last();
                ci.Label = key;
                ci.Value = "";

                // Reset randomized rect values
                ci.GetComponent<RectTransform>().rect.Set(pfRect.x, pfRect.y, pfRect.width, pfRect.height);

                // Space items out vertically
                Vector3 pos = ci.transform.localPosition;
                ci.transform.localPosition = new Vector3(pos.x, pos.y + (-65 * index), pos.z);

                index++;
            }

            // Setup sequence
            var resultSequence = CreateResultsSequence(results, currencyItems, currencyAmountAtStart);
        }


        private Sequence CreateResultsSequence(IDictionary<string, int> results, List<CurrencyItem> items, int currencyAmountAtStart)
        {
            var seq = DOTween.Sequence();

            items.ForEach(item =>
            {
                item.gameObject.SetActive(false);

                seq.AppendCallback(() => { item.gameObject.SetActive(true); });
                seq.Append(CreateResultItemTweener(item, results[item.Label]));
            });

            seq.Append(CreateTotalResultTweener(results, currencyAmountAtStart));

            return seq;
        }


        private Tweener CreateResultItemTweener(CurrencyItem item, int finalValue)
        {
            return DOTween.To(x => item.Value = Mathf.RoundToInt(x).ToString(), 0, finalValue, CurrencyCounterSeconds);
        }


        private Tweener CreateTotalResultTweener(IDictionary<string, int> results, int currencyAmountAtStart)
        {
            var totalSum = results.Values.Sum() + currencyAmountAtStart;

            return DOTween.To(x => _currencyText.text = Mathf.RoundToInt(x).ToString(), currencyAmountAtStart, totalSum, CurrencyCounterSeconds);
        }
    }
}