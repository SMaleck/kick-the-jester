using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Mvc.Models
{
    public class UserInputModel : AbstractDisposable
    {
        private readonly Subject<Unit> _onClickedAnywhere;
        public IObservable<Unit> OnClickedAnywhere => _onClickedAnywhere;

        private readonly Subject<Unit> _onPause;
        public IObservable<Unit> OnPause => _onPause;

        public UserInputModel()
        {
            _onClickedAnywhere = new Subject<Unit>().AddTo(Disposer);
            _onPause = new Subject<Unit>().AddTo(Disposer);
        }

        public void PublishOnClickedAnywhere()
        {
            _onClickedAnywhere.OnNext(Unit.Default);
        }

        public void PublishOnPause()
        {
            _onPause.OnNext(Unit.Default);
        }
    }
}
