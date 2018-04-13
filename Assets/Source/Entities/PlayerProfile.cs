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
        private float kickForce = 600f;
        private int kickForceInFlight = 1;
        private int kickCount = 1;
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

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            // Load any persisted data
            LoadKickForce();
            LoadKickForceInFlight();
            LoadKickCount();
            LoadBestDistance();
        }

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
    }
}
