using Assets.Source.Util;
using System;
using UniRx;

namespace Assets.Source.Mvc.Models.ViewModels
{
    public class OpenPanelModel : AbstractDisposable
    {
        private readonly Subject<Unit> _onOpenUpgrades;
        public IObservable<Unit> OnOpenUpgrades => _onOpenUpgrades;

        private readonly Subject<Unit> _onOpenSettings;
        public IObservable<Unit> OnOpenSettings => _onOpenSettings;

        private readonly Subject<Unit> _onOpenRoundEnd;
        public IObservable<Unit> OnOpenRoundEnd => _onOpenRoundEnd;

        private readonly Subject<Unit> _onOpenResetConfirmation;
        public IObservable<Unit> OnOpenResetConfirmation => _onOpenResetConfirmation;

        private readonly Subject<Unit> _onOpenCredits;
        public IObservable<Unit> OnOpenCredits => _onOpenCredits;

        private readonly Subject<Unit> _onOpenTutorial;
        public IObservable<Unit> OnOpenTutorial => _onOpenTutorial;


        public OpenPanelModel()
        {
            _onOpenUpgrades = new Subject<Unit>().AddTo(Disposer);
            _onOpenSettings = new Subject<Unit>().AddTo(Disposer);
            _onOpenRoundEnd = new Subject<Unit>().AddTo(Disposer);
            _onOpenResetConfirmation = new Subject<Unit>().AddTo(Disposer);
            _onOpenCredits = new Subject<Unit>().AddTo(Disposer);
            _onOpenTutorial = new Subject<Unit>().AddTo(Disposer);
        }

        public void OpenUpgrades()
        {
            _onOpenUpgrades.OnNext(Unit.Default);
        }

        public void OpenSettings()
        {
            _onOpenSettings.OnNext(Unit.Default);
        }

        public void OpenRoundEnd()
        {
            _onOpenRoundEnd.OnNext(Unit.Default);
        }

        public void OpenResetConfirmation()
        {
            _onOpenResetConfirmation.OnNext(Unit.Default);
        }

        public void OpenCredits()
        {
            _onOpenCredits.OnNext(Unit.Default);
        }

        public void OpenTutorial()
        {
            _onOpenTutorial.OnNext(Unit.Default);
        }
    }
}
