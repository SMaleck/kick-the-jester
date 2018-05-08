using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.App;
using Assets.Source.Models;
using UniRx;
using System;

namespace Assets.Source.Repositories
{
    public class PlayerProfileRepository
    {
        #region PROPERTIES

        private FileDataStorage<PlayerProfile> storage;

        public FloatReactiveProperty kickForceProperty = new FloatReactiveProperty(PlayerProfile.BASE_KICK_FORCE);
        public float KickForce
        {
            get { return kickForceProperty.Value; }
            set
            {
                kickForceProperty.Value = value;
            }
        }

        public IntReactiveProperty kickCountProperty = new IntReactiveProperty(PlayerProfile.BASE_KICK_COUNT);
        public int KickCount
        {
            get { return kickCountProperty.Value; }
            set
            {
                kickCountProperty.Value = value;
            }
        }

        public IntReactiveProperty bestDistanceProperty = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return bestDistanceProperty.Value; }
            set
            {
                bestDistanceProperty.Value = value;
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

        private event NotifyEventHandler _OnLoaded = delegate { };
        public void OnLoaded(NotifyEventHandler handler)
        {
            _OnLoaded += handler;
            if (isLoaded) { handler(); }
        }

        #endregion

        #region INITIALIZATION

        public PlayerProfileRepository(FileDataStorage<PlayerProfile> dataStorage)
        {
            if (dataStorage == null) { throw new System.ArgumentNullException("dataStorage"); }
            this.storage = dataStorage;

            LoadProfile();
            EnsureProfileDataPersistence();
        }

        /// <summary>
        /// Ensures that data is saved to disk as required
        /// </summary>
        private void EnsureProfileDataPersistence()
        {            
            // Game over
            
            // Purchase

            // Scene load
            App.Cache.screenManager.OnStartLoading(SaveProfile);

            // Application quit
            Observable.OnceApplicationQuit().Subscribe(x => { SaveProfile(); });
        }

        private void LoadProfile()
        {
            // Load any persisted data
            PlayerProfile loadedProfile = storage.Load();

            BestDistance = loadedProfile.BestDistance;
            KickCount = loadedProfile.KickCount;
            KickForce = loadedProfile.KickForce;
            Currency = loadedProfile.Currency;
            
            isLoaded = true;
            _OnLoaded();
        }

        #endregion
        
        public void ResetStats()
        {
            KickCount = PlayerProfile.BASE_KICK_COUNT;
            KickForce = PlayerProfile.BASE_KICK_FORCE;
            BestDistance = 0;
        }

        /// <summary>
        /// Persists current profile data to disk.
        /// See <see cref="PlayerProfile"/>
        /// <para>
        /// See also <seealso cref="FileDataStorage{T}"/>
        /// </para>
        /// </summary>
        public void SaveProfile()
        {
            PlayerProfile profile = new PlayerProfile
            {
                BestDistance = BestDistance,
                KickCount = KickCount,
                KickForce = KickForce,
                Currency = Currency
            };
            storage.Save(profile);
        }
    }
}