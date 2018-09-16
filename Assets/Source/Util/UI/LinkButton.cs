using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Util.UI
{
    public class LinkButton : Button
    {
        [SerializeField] public string Url;

        // TODO Fix Inspector with https://answers.unity.com/questions/1304097/subclassing-button-public-variable-wont-show-up-in.html
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
