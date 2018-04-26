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

        public FloatReactiveProperty kickForceProperty = new FloatReactiveProperty(BASE_KICK_FORCE);
        public float KickForce
        {
            get { return kickForceProperty.Value; }
            set
            {
                kickForceProperty.Value = value;
                PlayerPrefs.SetFloat(Constants.PREF_KICK_FORCE, value);
            }
        }

        public IntReactiveProperty kickCountProperty = new IntReactiveProperty(BASE_KICK_COUNT);
        public int KickCount
        {
            get { return kickCountProperty.Value; }
            set
            {
                kickCountProperty.Value = value;
                PlayerPrefs.SetInt(Constants.PREF_KICK_COUNT, value);
            }
        }

        public IntReactiveProperty bestDistanceProperty = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return bestDistanceProperty.Value; }
            set
            {
                bestDistanceProperty.Value = value;
                PlayerPrefs.SetInt(Constants.PREF_BEST_DISTANCE, value);
            }
        }

        // CURRENCY
        private event IntValueEventHandler _OnCurrencyChanged = delegate { };
        public void OnCurrencyChanged(IntValueEventHandler handler)
        {
            _OnCurrencyChanged += handler;
            handler(Currency);
        }

        public IntReactiveProperty currencyProperty = new IntReactiveProperty(0);
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
            LoadKickCount();
            LoadBestDistance();
            LoadCurrency();

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