using Assets.Source.Entities.GenericComponents;
using Assets.Source.Features.PlayerData;
using Assets.Source.Util;
using UniRx;

namespace Assets.Source.Entities.Jester.Components
{
    public class FlightRecorder : AbstractPausableComponent<JesterEntity>
    {
        private readonly FlightStatsModel _flightStatsModel;

        private bool _canCheckForIsLanded = false;

        public FlightRecorder(JesterEntity owner, FlightStatsModel flightStatsModel)
            : base(owner)
        {
            _flightStatsModel = flightStatsModel;

            _flightStatsModel.SetOrigin(owner.GoTransform.position);

            owner.OnKicked
                .Subscribe(_ => _canCheckForIsLanded = true)
                .AddTo(owner);

            Observable.EveryLateUpdate()
                .Where(_ => !IsPaused.Value)
                .Subscribe(_ => LateUpdate())
                .AddTo(owner);

            _flightStatsModel.IsLanded
                .Where(isLanded => _canCheckForIsLanded && isLanded)
                .Take(1)
                .Subscribe(_ => Owner.OnLanded.Execute())
                .AddTo(owner);
        }


        private void LateUpdate()
        {
            _flightStatsModel.SetPosition(Owner.GoTransform.position);
            _flightStatsModel.SetVelocity(Owner.GoBody.velocity);
        }
    }
}