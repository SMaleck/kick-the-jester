using Assets.Source.Util;
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
        [SerializeField] GameObject _pfCurrencyItem;
        [SerializeField] TMP_Text _currencyText;

        [Header("Buttons")]
        [SerializeField] Button _retryButton;
        [SerializeField] Button _shopButton;

        public ReactiveCommand OnRetryClicked = new ReactiveCommand();
        public ReactiveCommand OnShopClicked = new ReactiveCommand();

        public float Distance { set { _distanceText.text = value.ToMetersString(); } }
        public float BestDistance { set { _bestDistanceText.text = value.ToMetersString(); } }
        public bool IsNewBestDistance { set { _newBestLabel.SetActive(value); } }


        public override void Setup()
        {
            base.Setup();

            _retryButton.OnClickAsObservable()
                .Subscribe(_ => OnRetryClicked.Execute())
                .AddTo(this);

            _shopButton.OnClickAsObservable()
                .Subscribe(_ => OnShopClicked.Execute())
                .AddTo(this);
        }        
    }
}
