﻿using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Models
{
    public class FlightStatsModel
    {
        public FloatReactiveProperty Distance = new FloatReactiveProperty();
        public FloatReactiveProperty Height = new FloatReactiveProperty();
        public ReactiveProperty<Vector2> Velocity = new ReactiveProperty<Vector2>();

        public BoolReactiveProperty IsStarted = new BoolReactiveProperty();
        public BoolReactiveProperty IsLanded = new BoolReactiveProperty();        
    }
}
