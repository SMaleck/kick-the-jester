using Assets.Source.Mvc.Models.Enum;
using Assets.Source.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Source.Mvc.Views.PartialViews
{
    public class CurrencyGainItemView : AbstractView
    {
        public class Factory : PlaceholderFactory<UnityEngine.Object, CurrencyGainItemView> { }

        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _valueText;

        public override void Setup()
        { }

        public Tween SetupValueCountingTween(CurrencyGainType currencyGainType, int amount, float durationSeconds)
        {
            _labelText.text = TextService.FromCurrencyGainType(currencyGainType);
            _valueText.text = string.Empty;

            return DOTween.To(
                x => _valueText.text = TextService.CurrencyAmount(x),
                0,
                amount,
                durationSeconds)
                .Pause();
        }
    }
}
