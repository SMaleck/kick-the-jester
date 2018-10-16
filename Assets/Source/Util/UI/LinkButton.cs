using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    public class LinkButton : Button
    {
        [SerializeField] public string Url;

        // ToDo Automatically append external link icon (Not FA, use inline Sprites)
        protected override void Awake()
        {
            base.Awake();

            #if !UNITY_WEBGL

            this.OnClickAsObservable()
                .Subscribe(_ => { Application.OpenURL(Url); })
                .AddTo(this);
            
            #endif
        }
    }
}
