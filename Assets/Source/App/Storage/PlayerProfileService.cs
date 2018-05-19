using System;
using UniRx;

namespace Assets.Source.App.Storage
{
    public class PlayerProfileService : AbstractPersistentDataService
    {
        #region PROPERTIES

        PlayerProfile lastSavedProfileData;
        private FileDataStorage<PlayerProfile> storage;

        public FloatReactiveProperty kickForceProperty = new FloatReactiveProperty(PlayerProfile.BASE_KICK_FORCE);
        public float KickForce
        {
            get { return kickForceProperty.Value; }
            set { kickForceProperty.Value = value; }
        }

        public IntReactiveProperty kickCountProperty = new IntReactiveProperty(PlayerProfile.BASE_KICK_COUNT);
        public int KickCount
        {
            get { return kickCountProperty.Value; }
            set { kickCountProperty.Value = value; }
        }

        public IntReactiveProperty bestDistanceProperty = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return bestDistanceProperty.Value; }
            set { bestDistanceProperty.Value = value; }
        }

        public IntReactiveProperty currencyProperty = new IntReactiveProperty(0);
        public int Currency
        {
            get { return currencyProperty.Value; }
            set { currencyProperty.Value = value; }
        }

        #endregion


        #region INITIALIZATION

        public PlayerProfileService(FileDataStorage<PlayerProfile> dataStorage)
            : base()
        {
            if (dataStorage == null) { throw new System.ArgumentNullException("dataStorage"); }
            this.storage = dataStorage;

            LoadProfile();
            EnsureProfileDataPersistence();

            IsLoadedProperty.Value = true;
        }


        /// <summary>
        /// Persists current profile data to disk.
        /// See <see cref="PlayerProfile"/>
        /// <para>
        /// See also <seealso cref="FileDataStorage{T}"/>
        /// </para>
        /// </summary>
        public override void Save()
        {
            if (!MustSave) return;

            lastSavedProfileData.BestDistance = BestDistance;
            lastSavedProfileData.KickCount = KickCount;
            lastSavedProfileData.KickForce = KickForce;
            lastSavedProfileData.Currency = Currency;

            storage.Save(lastSavedProfileData);
        }


        private void LoadProfile()
        {
            // Load any persisted data
            lastSavedProfileData = storage.Load();

            BestDistance = lastSavedProfileData.BestDistance;
            KickCount = lastSavedProfileData.KickCount;
            KickForce = lastSavedProfileData.KickForce;
            Currency = lastSavedProfileData.Currency;
        }

        /// <summary>
        /// Ensures that data is saved to disk as required
        /// </summary>
        private void EnsureProfileDataPersistence()
        {
            // Property changes
            bestDistanceProperty.Subscribe(__ => { Save(); });
            currencyProperty.Subscribe(__ => { Save(); });
            kickCountProperty.Subscribe(__ => { Save(); });
            kickForceProperty.Subscribe(__ => { Save(); });
        }

        #endregion
        

        public void ResetStats()
        {
            KickCount = PlayerProfile.BASE_KICK_COUNT;
            KickForce = PlayerProfile.BASE_KICK_FORCE;
            BestDistance = 0;
        }
        

        private Boolean MustSave
        {
            get
            {
                return BestDistance != lastSavedProfileData.BestDistance
                        || KickCount != lastSavedProfileData.KickCount
                        || KickForce != lastSavedProfileData.KickForce
                        || Currency != lastSavedProfileData.Currency;
            }
        }
    }
}