using Assets.Source.App.Upgrade;
using System;
using UniRx;

namespace Assets.Source.App.Storage
{
    public class PlayerProfileService : AbstractPersistentDataService
    {
        private readonly FileDataStorage<PlayerProfile> storage;
        private PlayerProfile lastSavedProfileData;


        /* -------------------------------------------------------------------------- */
        #region PROFILE DATA

        /* TOGGLES */
        public BoolReactiveProperty RP_IsFirstStart = new BoolReactiveProperty(true);
        public bool IsFirstStart
        {
            get { return RP_IsFirstStart.Value; }
            set { RP_IsFirstStart.Value = value; }
        }

        /* UPGRADEABLE PROPERTIES */
        public IntReactiveProperty RP_MaxVelocityLevel = new IntReactiveProperty(0);
        public IntReactiveProperty RP_KickForceLevel = new IntReactiveProperty(0);
        public IntReactiveProperty RP_ShootForceLevel = new IntReactiveProperty(0);
        public IntReactiveProperty RP_ShootCountLevel = new IntReactiveProperty(0);

        
        /* DERIVED VALUES */

        public float MaxVelocity
        {
            get { return UpgradeTree.MaxVelocityPath.ValueAt(RP_MaxVelocityLevel.Value); }
        }

        public float KickForce
        {
            get { return UpgradeTree.KickForcePath.ValueAt(RP_KickForceLevel.Value); }
        }

        public float ShootForce
        {
            get { return UpgradeTree.ShootForcePath.ValueAt(RP_ShootForceLevel.Value); }
        }

        public int ShootCount
        {
            get { return UpgradeTree.ShootCountPath.ValueAt(RP_ShootCountLevel.Value); }
        }


        /* RECORDED DATA */
        public IntReactiveProperty RP_BestDistance = new IntReactiveProperty(0);
        public int BestDistance
        {
            get { return RP_BestDistance.Value; }
            set { RP_BestDistance.Value = value; }
        }

        public IntReactiveProperty RP_Currency = new IntReactiveProperty(0);
        public int Currency
        {
            get { return RP_Currency.Value; }
            set { RP_Currency.Value = value; }
        }

        #endregion


        // CONSTRUCTOR --------------------------------------------------------
        public PlayerProfileService(FileDataStorage<PlayerProfile> dataStorage)
            : base()
        {            
            this.storage = dataStorage;

            LoadProfile();
            EnsureProfileDataPersistence();

            IsLoadedProperty.Value = true;
        }


        private void LoadProfile()
        {
            // Load any persisted data
            lastSavedProfileData = storage.Load();

            RP_IsFirstStart.Value = lastSavedProfileData.IsFirstStart;

            RP_MaxVelocityLevel.Value = lastSavedProfileData.MaxVelocityLevel;
            RP_KickForceLevel.Value = lastSavedProfileData.KickForceLevel;
            RP_ShootForceLevel.Value = lastSavedProfileData.ShootForceLevel;
            RP_ShootCountLevel.Value = lastSavedProfileData.ShootCountLevel;    
            
            BestDistance = lastSavedProfileData.BestDistance;
            Currency = lastSavedProfileData.Currency;
        }

        /// <summary>
        /// Ensures that data is saved to disk as required
        /// </summary>
        private void EnsureProfileDataPersistence()
        {
            RP_IsFirstStart.Subscribe(_ => Save());
            RP_MaxVelocityLevel.Subscribe(_ => Save());
            RP_KickForceLevel.Subscribe(_ => Save());
            RP_ShootForceLevel.Subscribe(_ => Save());
            RP_ShootCountLevel.Subscribe(_ => Save());
            
            RP_BestDistance.Subscribe(_ => Save());
            RP_Currency.Subscribe(_ => Save());
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
            if (!MustSave) { return; }

            lastSavedProfileData.IsFirstStart = RP_IsFirstStart.Value;

            lastSavedProfileData.MaxVelocityLevel = RP_MaxVelocityLevel.Value;
            lastSavedProfileData.KickForceLevel = RP_KickForceLevel.Value;
            lastSavedProfileData.ShootForceLevel = RP_ShootForceLevel.Value;
            lastSavedProfileData.ShootCountLevel = RP_ShootCountLevel.Value;            

            lastSavedProfileData.BestDistance = BestDistance;
            lastSavedProfileData.Currency = Currency;

            storage.Save(lastSavedProfileData);
        }


        public void ResetStats()
        {
            RP_MaxVelocityLevel.Value = 0;
            RP_KickForceLevel.Value = 0;
            RP_ShootForceLevel.Value = 0;
            RP_ShootCountLevel.Value = 0;
            RP_Currency.Value = 0;

            BestDistance = 0;
        }
        

        private Boolean MustSave
        {
            get
            {
                return RP_IsFirstStart.Value != lastSavedProfileData.IsFirstStart
                    || RP_MaxVelocityLevel.Value != lastSavedProfileData.MaxVelocityLevel
                    || RP_KickForceLevel.Value != lastSavedProfileData.KickForceLevel
                    || RP_ShootForceLevel.Value != lastSavedProfileData.ShootForceLevel
                    || RP_ShootCountLevel.Value != lastSavedProfileData.ShootCountLevel
                    || BestDistance != lastSavedProfileData.BestDistance
                    || Currency != lastSavedProfileData.Currency;
            }
        }
    }
}