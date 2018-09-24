using Assets.Source.App;
using Assets.Source.UI.Elements;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    // TODO a lot
    public class HudView : AbstractView
    {
        [Header("Flight Stats Display")]
        [SerializeField] TMP_Text DistanceText;
        [SerializeField] TMP_Text HeightText;
        [SerializeField] TMP_Text BestDistanceText;
        
        [Header("Tomatoes")]
        [SerializeField] GameObject shotCountPanel;
        [SerializeField] GameObject PF_ShotCountIcon;
        private List<Image> shotCountIcons = new List<Image>();

        [Header("Other")]
        [SerializeField] Button PauseButton;
        [SerializeField] UIProgressBar velocityBar;

        [Header("Money Gain Floating Numbers")]
        [SerializeField] RectTransform MoneyGainPanel;
        [SerializeField] GameObject PF_MoneyGainText;
        private List<FloatingValue> moneyGainSlots = new List<FloatingValue>();        

        public float Distance
        {
            set { DistanceText.text = value.ToMetersString(); }
        }

        public float Height
        {
            set { HeightText.text = value.ToMetersString(); }
        }

        public float BestDistance
        {
            set { BestDistanceText.text = value.ToMetersString(); }
        }

        public float RelativeVelocity
        {
            set { velocityBar.fillAmount = value; }
        }

        public ReactiveCommand OnPauseButtonClicked = new ReactiveCommand();


        public override void Setup()
        {            
            PauseButton.OnClickAsObservable()
                       .Subscribe(_ => OnPauseButtonClicked.Execute())
                       .AddTo(this);

            
            velocityBar.gameObject.SetActive(false);
            shotCountPanel.gameObject.SetActive(false);
        }
       

        public void StartRound()
        {
            velocityBar.gameObject.SetActive(true);
            shotCountPanel.gameObject.SetActive(true);
        }

        
        public void ShowFloatingCoinAmount(float gainedAmount)
        {
            if (gainedAmount <= 0) { return; }

            FloatingValue fValue = moneyGainSlots.FirstOrDefault(e => !e.IsFloating);

            if (fValue == null)
            {
                GameObject go = GameObject.Instantiate(PF_MoneyGainText, MoneyGainPanel, false);
                moneyGainSlots.Add(go.GetComponent<FloatingValue>());

                fValue = moneyGainSlots.Last();
            }

            fValue.StartFloating(string.Format("{0} {1}", gainedAmount, Constants.TMP_SPRITE_COIN));
        }
      

        public void OnShotCountChanged(int count)
        {
            int diff = Mathf.Abs(count - shotCountIcons.Count);

            // Add additional Icons, if the count is higher than the current amount
            if (shotCountIcons.Count < count)
            {
                AddShotCountIcons(diff);
                return;
            }

            // Reduce opacity if the count is lower
            var toDeactivate = shotCountIcons.Skip(count).Take(diff);

            foreach (Image img in toDeactivate)
            {
                img.color = new Color(1, 1, 1, 0.5f);
            }
        }

        private void AddShotCountIcons(int countToAdd)
        {
            float width = 0;

            for (int i = 0; i < countToAdd; i++)
            {
                var go = GameObject.Instantiate(PF_ShotCountIcon);
                go.transform.SetParent(shotCountPanel.transform);

                // Get the width if we did not do it yet, because we cannot get it reliably from the prefab
                if (width <= 0)
                {
                    width = go.GetComponent<RectTransform>().rect.width;
                }

                go.transform.localPosition = new Vector3(i * width, 0, 0);
                go.transform.localScale = Vector3.one;

                shotCountIcons.Add(go.GetComponent<Image>());
            }

            // Set all to full Opacity
            foreach (Image img in shotCountIcons)
            {
                img.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
