using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.App;
using Assets.Source.Models;
using UniRx;

namespace Assets.Source.Repositories
{
    public class PlayerProfileRepository : MonoBehaviour
    {
        #region CONSTANTS

        private const float BASE_KICK_FORCE = 600f;
        private const int BASE_KICK_COUNT = 2;

        #endregion

        #region PROPERTIES

        private FloatReactiveProperty kickForceProperty = new FloatReactiveProperty(BASE_KICK_FORCE);
        public float KickForce
        {
            get { return kickForceProperty.Value; }
            set
            {
                kickForceProperty.Value = value;
                PlayerPrefs.SetFloat(Constants.PREF_KICK_FORCE, value);
            }
        }

        private IntReactiveProperty kickCountProperty = new IntReactiveProperty(BASE_KICK_COUNT);
        public int KickCount
        {
            get { return kickCountProperty.Value; }
            set
            {
                kickCountProperty.Value = value;
                PlayerPrefs.SetInt(Constants.PREF_KICK_COUNT, value);
            }
        }


        // BEST DISTANCE
        private event IntValueEventHandler _OnBestDistanceChanged = delegate { };
        public void OnBestDistanceChanged(IntValueEventHandler handler)
        {
            _OnBestDistanceChanged += handler;
            handler(BestDistance);
        }

        private IntReactiveProperty bestDistanceProperty = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return bestDistanceProperty.Value; }
            set
            {
                bestDistanceProperty.Value = value;
                PlayerPrefs.SetInt(Constants.PREF_BEST_DISTANCE, value);
                _OnBestDistanceChanged(bestDistanceProperty.Value);
            }
        }

        // CURRENCY
        private event IntValueEventHandler _OnCurrencyChanged = delegate { };
        public void OnCurrencyChanged(IntValueEventHandler handler)
        {
            _OnCurrencyChanged += handler;
            handler(Currency);
        }

        private IntReactiveProperty currencyProperty = new IntReactiveProperty(0);
        public int Currency
        {
            get { return currencyProperty.Value; }
            set
            {
                currencyProperty.Value = value;
                PlayerPrefs.SetInt(Constants.PREF_CURRENCY, value);
                _OnCurrencyChanged(currencyProperty.Value);
            }
        }

        #endregion

        #region EVENTS

        private bool isLoaded = false;
        public bool IsLoaded
        {
            get { return isLoaded; }
            private set { isLoaded = value; }
        }

        public delegate void ProfileLoadedEventHandler(PlayerProfileRepository profile);
        private event ProfileLoadedEventHandler _OnProfileLoaded = delegate { };

        public void OnProfileLoaded(ProfileLoadedEventHandler handler)
        {
            _OnProfileLoaded += handler;

            // In case it is attached too late, call it now
            if (isLoaded)
            {
                handler(this);
            }
        }

        #endregion

        #region UNITY LIFECYCLE HOOKS

        void Awake()
        {
            // Keep max one instance of this class/gameobject
            if (FindObjectsOfType(GetType()).Length <= 1)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void Start()
        {
            // Load any persisted data
            LoadKickForce();
            //LoadKickForceInFlight();
            LoadKickCount();
            LoadBestDistance();
            LoadCurrency();

            _OnProfileLoaded(this);
            isLoaded = true;
        }

        #endregion

        #region LOADERS

        private void LoadBestDistance()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_BEST_DISTANCE))
            {
                BestDistance = PlayerPrefs.GetInt(Constants.PREF_BEST_DISTANCE);
            }
        }

        private void LoadKickCount()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_KICK_COUNT))
            {
                kickCountProperty.Value = PlayerPrefs.GetInt(Constants.PREF_KICK_COUNT);
            }
        }

        //private void LoadKickForceInFlight()
        //{
        //    if (PlayerPrefs.HasKey(Constants.PREF_KICK_FORCE_INFLIGHT))
        //    {
        //        kickForceInFlight = PlayerPrefs.GetInt(Constants.PREF_KICK_FORCE_INFLIGHT);
        //    }
        //}

        private void LoadKickForce()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_KICK_FORCE))
            {
                kickForceProperty.Value = PlayerPrefs.GetFloat(Constants.PREF_KICK_FORCE);
            }
        }

        private void LoadCurrency()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_CURRENCY))
            {
                Currency = PlayerPrefs.GetInt(Constants.PREF_CURRENCY);
            }
        }

        #endregion

        public void ResetStats()
        {
            KickCount = BASE_KICK_COUNT;
            KickForce = BASE_KICK_FORCE;
            BestDistance = 0;
        }
    }
}