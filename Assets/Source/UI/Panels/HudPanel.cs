using Assets.Source.App;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class HudPanel : AbstractPanel
    {
        [Header("Panel Properties")]
        [SerializeField] Button PauseButton;

        [SerializeField] Text DistanceText;
        [SerializeField] Text HeightText;
        [SerializeField] Text BestDistanceText;

        [SerializeField] UIProgressBar velocityBar;

        [SerializeField] GameObject shotCountPanel;
        [SerializeField] GameObject PF_ShotCountIcon;

        private List<Image> shotCountIcons = new List<Image>();

        public override void Setup()
        {
            base.Setup();

            PauseButton.OnClickAsObservable()
                       .Subscribe(_ => App.Cache.userControl.TooglePause())
                       .AddTo(this);
            
            App.Cache.Jester.DistanceProperty
                            .SubscribeToText(DistanceText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            App.Cache.Jester.HeightProperty
                            .SubscribeToText(HeightText, e => string.Format("{0}m", e.ToMeters()))
                            .AddTo(this);

            Kernel.PlayerProfile.Stats.RP_BestDistance
                                      .SubscribeToText(BestDistanceText, e => string.Format("{0}m", e.ToMeters()))
                                      .AddTo(this);

            App.Cache.Jester.RelativeVelocityProperty
                            .Subscribe((float value) => { velocityBar.fillAmount = value; })
                            .AddTo(this);

           
            // Activate Velocity and Ammo display only after start
            velocityBar.gameObject.SetActive(false);
            App.Cache.Jester.IsStartedProperty.Where(e => e).Subscribe(_ => 
            {
                velocityBar.gameObject.SetActive(true);


                // Subscribe to ammo count
                App.Cache.Jester.AvailableShotsProperty
                           .Subscribe(OnShotCountChanged)
                           .AddTo(this);
            });
        }


        private void AddShotCountIcons(int countToAdd)
        {            
            float width = 0;

            for (int i = 0; i < countToAdd; i++)
            {
                var go = GameObject.Instantiate(PF_ShotCountIcon);
                go.transform.SetParent(shotCountPanel.transform);

                // Get the width if we did not do it yet, because we cannot get it reliably from the prefab
                if(width <= 0)
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


        private void OnShotCountChanged(int count)
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
    }
}
