using Assets.Source.Entities.GenericComponents;
using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.Entities.GameRound.Components
{
    public class RoundStatsRecorder : AbstractComponent<GameRoundEntity>
    {
        private readonly GameStateModel _gameStateModel;
        private readonly FlightStatsModel _flightStatsModel;
        private readonly ProfileModel _profileModel;

        public RoundStatsRecorder(GameRoundEntity owner, GameStateModel gameStateModel, FlightStatsModel flightStatsModel, ProfileModel profileModel) 
            : base(owner)
        {
            _gameStateModel = gameStateModel;
            _flightStatsModel = flightStatsModel;
            _profileModel = profileModel;

            _gameStateModel.OnRoundEnd
                .Subscribe(_ => OnRoundEnd())
                .AddTo(owner);
        }

        private void OnRoundEnd()
        {
            // Record BEST DISTANCE
            var distance = _flightStatsModel.Distance.Value;
            var lastBestDistance = _profileModel.BestDistance.Value;

            _profileModel.BestDistance.Value = Mathf.Max(distance, lastBestDistance);

            // Record PLAY COUNT
            _profileModel.RoundsPlayed.Value++;            
        }
    }
}
