using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Source.App;

namespace Assets.Source.Entities
{
    public class PlayerProfile : BaseEntity
    {
        #region CONSTANTS
        private const float BASE_KICK_FORCE = 600f;
        private const int BASE_KICK_COUNT = 2;
        #endregion

        #region PROPERTIES
        //[SerializeField]
        private float kickForce = BASE_KICK_FORCE;
        private int kickForceInFlight = 1; // TODO: Is this one needed at all?
        //[SerializeField]
        private int kickCount = BASE_KICK_COUNT;
        //[SerializeField]
        private int bestDistance = 0;
        
        public float KickForce
        {
            get { return kickForce; }
            set
            {
                kickForce = value;
                PlayerPrefs.SetFloat(Constants.PREF_KICK_FORCE, value);
            }
        }
        public int KickCount
        {
            get { return kickCount; }
            set
            {
                kickCount = value;
                PlayerPrefs.SetInt(Constants.PREF_KICK_COUNT, value);
            }
        }
        public int BestDistance
        {
            get { return bestDistance; }
            set
            {
                bestDistance = value;
                PlayerPrefs.SetInt(Constants.PREF_BEST_DISTANCE, value);
            }
        }

        #endregion

        #region EVENTS

        private bool isLoaded = false;

        public delegate void ProfileLoadedEventHandler(PlayerProfile profile);
        private event ProfileLoadedEventHandler OnProfileLoaded = delegate { };

        public void AddEventHandler(ProfileLoadedEventHandler handler)
        {
            OnProfileLoaded += handler;

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
            LoadKickForceInFlight();
            LoadKickCount();
            LoadBestDistance();

            OnProfileLoaded(this);
            isLoaded = true;
        }

        #endregion

        #region LOADERS

        private void LoadBestDistance()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_BEST_DISTANCE))
            {
                bestDistance = PlayerPrefs.GetInt(Constants.PREF_BEST_DISTANCE);
            }
        }

        private void LoadKickCount()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_KICK_COUNT))
            {
                kickCount = PlayerPrefs.GetInt(Constants.PREF_KICK_COUNT);
            }
        }

        private void LoadKickForceInFlight()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_KICK_FORCE_INFLIGHT))
            {
                kickForceInFlight = PlayerPrefs.GetInt(Constants.PREF_KICK_FORCE_INFLIGHT);
            }
        }

        private void LoadKickForce()
        {
            if (PlayerPrefs.HasKey(Constants.PREF_KICK_FORCE))
            {
                kickForce = PlayerPrefs.GetFloat(Constants.PREF_KICK_FORCE);
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
