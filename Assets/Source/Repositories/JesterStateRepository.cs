using Assets.Source.Models;
using UniRx;
using UnityEngine;

namespace Assets.Source.Repositories
{
    public class JesterStateRepository : MonoBehaviour
    {
        /* ------------------------------------------------------------------------------------- */
        #region GENERAL FLIGHT STATE

        // STARTED FLIGHT
        private event NotifyEventHandler _OnStartedFlight = delegate { };
        public void OnStartedFlight(NotifyEventHandler handler)
        {
            _OnStartedFlight += handler;
        }

        private bool isStarted = false;
        public bool IsStarted
        {
            get { return isStarted; }
            set
            {
                bool previous = isStarted;
                isStarted = value;

                // Notify if we started and the value changed
                if (value && (isStarted != previous)) { _OnStartedFlight(); }
            }
        }


        // IS LANDED
        private event NotifyEventHandler _OnLanded = delegate { };
        public void OnLanded(NotifyEventHandler handler)
        {
            _OnLanded += handler;
        }

        private bool isLanded = false;
        public bool IsLanded
        {
            get { return isLanded; }
            set
            {
                bool previous = isLanded;
                isLanded = value;

                // Notify if we landed and the value changed
                if (value && (isLanded != previous)) { _OnLanded(); }
            }
        }

        #endregion


        /* ------------------------------------------------------------------------------------- */
        #region FLIGHT RECORDER DATA

        public FloatReactiveProperty DistanceProperty = new FloatReactiveProperty(0);
        public float Distance
        {
            get { return DistanceProperty.Value; }
            set { DistanceProperty.Value = value; }
        }


        public FloatReactiveProperty HeightProperty = new FloatReactiveProperty(0);
        public float Height
        {
            get { return HeightProperty.Value; }
            set { HeightProperty.Value = value; }
        }


        public Vector2ReactiveProperty VelocityProperty = new Vector2ReactiveProperty();
        public Vector2 Velocity
        {
            get { return VelocityProperty.Value; }
            set { VelocityProperty.Value = value; }
        }

        #endregion


        /* ------------------------------------------------------------------------------------- */
        #region CURRENCY

        public IntReactiveProperty CollectedCurrencyProperty = new IntReactiveProperty(0);
        public int CollectedCurrency
        {
            get { return CollectedCurrencyProperty.Value; }
            set { CollectedCurrencyProperty.Value = value; }
        }


        public IntReactiveProperty EarnedCurrencyProperty = new IntReactiveProperty(0);
        public int EarnedCurrency
        {
            get { return EarnedCurrencyProperty.Value; }
            set { EarnedCurrencyProperty.Value = value; }
        }

        #endregion


    }
}
