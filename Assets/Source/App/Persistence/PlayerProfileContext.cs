using Assets.Source.App.Persistence.Models;
using Assets.Source.App.Persistence.Storage;
using Assets.Source.App.Upgrade;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;

namespace Assets.Source.App.Persistence
{
    public class PlayerProfileContext : AbstractPersistentDataContext
    {
        protected class PlayerProfile
        {
            public Upgrades Upgrades = new Upgrades();
            public Stats Stats = new Stats();
            public Achievements Achievements = new Achievements();
        }


        /* ----------------------------------------------------------- */
        #region PERSISTENT PROPERTIES

        public Upgrades Upgrades { get; private set; }
        public Stats Stats { get; private set; }
        public Achievements Achievements { get; private set; }

        #endregion


        /* ----------------------------------------------------------- */
        #region DERIVED VALUES

        public float MaxVelocity
        {
            get { return UpgradeTree.MaxVelocityPath.ValueAt(Upgrades.MaxVelocityLevel); }
        }

        public float KickForce
        {
            get { return UpgradeTree.KickForcePath.ValueAt(Upgrades.KickForceLevel); }
        }

        public float ShootForce
        {
            get { return UpgradeTree.ShootForcePath.ValueAt(Upgrades.ShootForceLevel); }
        }

        public int ShootCount
        {
            get { return UpgradeTree.ShootCountPath.ValueAt(Upgrades.ShootCountLevel); }
        }

        #endregion


        /* ----------------------------------------------------------- */
        private readonly JsonStorage storage;

        public PlayerProfileContext(JsonStorage storage) 
            : base()
        {
            this.storage = storage; 

            // Load Profile
            PlayerProfile profile = Load();
            this.Upgrades = ThisOrNew(profile.Upgrades);
            this.Stats = ThisOrNew(profile.Stats);
            this.Achievements = ThisOrNew(profile.Achievements);

            SetupModelChangeListeners();
        }


        private T ThisOrNew<T>(T item) where T : new()
        {
            return (item != null) ? item : new T();
        }


        private void SetupModelChangeListeners()
        {
            // Get persistent models in this class
            IEnumerable<PropertyInfo> infos = this.GetType()
                                                  .GetProperties()
                                                  .Where(e => e.PropertyType.Equals(typeof(AbstractPersistentModel)));

            foreach (var info in infos)
            {
                var model = info.GetValue(this, null) as AbstractPersistentModel;
                model.OnAnyPropertyChanged += Save;
            }
        }


        // Saves existing data from the persistent storage
        protected PlayerProfile Load()
        {
            return storage.Load<PlayerProfile>();
        }


        // Saves the current data to the persistent storage
        protected override void Save()
        {
            storage.Save(new PlayerProfile
            {
                Upgrades = Upgrades,
                Stats = Stats,
                Achievements = Achievements
            });
        }


        /// <summary>
        /// Resets all player stats and upgrades to their starting values
        /// </summary>
        public void ResetProfile()
        {
            this.Upgrades = new Upgrades();
            this.Stats = new Stats();

            Save();
        }
    }
}
