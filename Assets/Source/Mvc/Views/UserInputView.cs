using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class UserInputView : AbstractView
    {
        [SerializeField] private Button _screenSpaceButton;

        public ReactiveCommand OnClickedAnywhere = new ReactiveCommand();

        // ToDo [IMPORTANT] Setup in UI Layer
        public override void Setup()
        {            
            _screenSpaceButton.OnClickAsObservable()
                .Subscribe(_ => OnClickedAnywhere.Execute())
                .AddTo(this);
        }
    }
}
