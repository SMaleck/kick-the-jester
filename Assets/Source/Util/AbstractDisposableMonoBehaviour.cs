using UniRx;
using UnityEngine;

namespace Assets.Source.Util
{
    public abstract class AbstractDisposableMonoBehaviour : MonoBehaviour
    {
        private CompositeDisposable _disposer;
        public CompositeDisposable Disposer => _disposer ?? 
            (_disposer = new CompositeDisposable().AddTo(this));
    }
}
