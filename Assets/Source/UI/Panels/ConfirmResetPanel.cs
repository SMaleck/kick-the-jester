using Assets.Source.App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.UI.Panels
{
    public class ConfirmResetPanel : AbstractPanel
    {
        [SerializeField] private Button ResetButton;
        [SerializeField] private Button CancelButton;

        public override void Setup()
        {
            this.gameObject.SetActive(false);

            ResetButton.OnClickAsObservable().Subscribe(_ => 
            {
                Kernel.PlayerProfileService.ResetStats();
                this.gameObject.SetActive(false);
            }).AddTo(this);

            CancelButton.OnClickAsObservable().Subscribe(_ => this.gameObject.SetActive(false)).AddTo(this);            
        }
    }
}
