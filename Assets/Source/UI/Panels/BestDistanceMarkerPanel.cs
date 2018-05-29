using Assets.Source.App;
using Assets.Source.Behaviours.GameLogic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class BestDistanceMarkerPanel : AbstractPanel
    {
        [SerializeField] private Text bestDistanceText;
        private int minMarkerThreshold = 10;        

        private void Start()
        {
            Setup();
        }

        public override void Setup()
        {
            transform.position = GetBestDistancePosition();

            // Bind value to text field
            Kernel.PlayerProfileService.RP_BestDistance
                                       .SubscribeToText(bestDistanceText, e => e.ToMetersString())
                                       .AddTo(this);

            // Hide MArker if it is very close to the start
            Kernel.PlayerProfileService.RP_BestDistance
                                       .Subscribe(e => { gameObject.SetActive(e >= minMarkerThreshold); })
                                       .AddTo(this);

            App.Cache.GameLogic.StateProperty
                               .Where(e => e.Equals(GameState.End))
                               .Subscribe(_ => MoveToPosition())
                               .AddTo(this);
        }


        // Move to new Position
        private void MoveToPosition()
        {
            Vector3 newBestPos = GetBestDistancePosition();
            LeanTween.value(this.gameObject, (Vector3 pos) => { transform.position = pos; }, transform.position, newBestPos, 0.6f)
                     .setEaseInOutCubic();
        }


        private Vector3 GetBestDistancePosition()
        {
            return new Vector3(Kernel.PlayerProfileService.BestDistance, transform.position.y, transform.position.z);
        }
    }
}
