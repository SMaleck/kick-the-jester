using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Mvc.Models.ViewModels
{
    public class UpgradeScreenModel : AbstractDisposable
    {
        private readonly Subject<Unit> _onOpenScreen;
        public IObservable<Unit> OnOpenScreen => _onOpenScreen;

        public UpgradeScreenModel()
        {
            _onOpenScreen = new Subject<Unit>().AddTo(Disposer);
        }

        public void ExecuteOpenScreen()
        {
            _onOpenScreen.OnNext(Unit.Default);
        }
    }
}
