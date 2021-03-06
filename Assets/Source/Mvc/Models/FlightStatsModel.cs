﻿using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Models
{
    public class FlightStatsModel
    {
        public FloatReactiveProperty Distance = new FloatReactiveProperty();
        public FloatReactiveProperty Height = new FloatReactiveProperty();
        public ReactiveProperty<Vector2> Velocity = new ReactiveProperty<Vector2>();
      
        public FloatReactiveProperty RelativeKickForce = new FloatReactiveProperty();
        public FloatReactiveProperty RelativeVelocity = new FloatReactiveProperty();
        public IntReactiveProperty ShotsRemaining = new IntReactiveProperty();

        public ReactiveCollection<int> Gains = new ReactiveCollection<int>();
        public IntReactiveProperty Collected = new IntReactiveProperty();
        public IntReactiveProperty Earned = new IntReactiveProperty();
    }
}
