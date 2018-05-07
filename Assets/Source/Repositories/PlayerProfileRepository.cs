using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Source.App;
using Assets.Source.Models;
using UniRx;

namespace Assets.Source.Repositories
{
    public class PlayerProfileRepository
    {
        #region PROPERTIES

        private PlayerProfile profile;
        private FileDataStorage<PlayerProfile> storage;

        public FloatReactiveProperty kickForceProperty = new FloatReactiveProperty(PlayerProfile.BASE_KICK_FORCE);
        public float KickForce
        {
            get { return kickForceProperty.Value; }
            set
            {
                kickForceProperty.Value = value;
                profile.KickForce = value;
            }
        }

        public IntReactiveProperty kickCountProperty = new IntReactiveProperty(PlayerProfile.BASE_KICK_COUNT);
        public int KickCount
        {
            get { return kickCountProperty.Value; }
            set
            {
                kickCountProperty.Value = value;
                profile.KickCount = value;
            }
        }

        public IntReactiveProperty bestDistanceProperty = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return bestDistanceProperty.Value; }
            set
            {
                bestDistanceProperty.Value = value;
                profile.BestDistance = value;
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
                profile.Currency = value;
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
        }

        private void LoadProfile()
        {
            // Load any persisted data
            profile = storage.LoadFromJson();
            LoadKickForce();
            LoadKickCount();
            LoadBestDistance();
            LoadCurrency();

            isLoaded = true;
            _OnLoaded();
        }

        #endregion

        #region LOADERS

        private void LoadBestDistance()
        {
            BestDistance = profile.BestDistance;
        }

        private void LoadKickCount()
        {
            KickCount = profile.KickCount;
        }

        private void LoadKickForce()
        {
            KickForce = profile.KickForce;
        }

        private void LoadCurrency()
        {
            Currency = profile.Currency;
        }

        #endregion

        public void ResetStats()
        {
            KickCount = PlayerProfile.BASE_KICK_COUNT;
            KickForce = PlayerProfile.BASE_KICK_FORCE;
            BestDistance = 0;
        }

        public void StoreProfile()
        {
            PlayerProfile profile = new PlayerProfile
            {
                BestDistance = BestDistance,
                KickCount = KickCount,
                KickForce = KickForce,
                Currency = Currency
            };
            storage.SaveAsJson(profile);
        }
    }
}