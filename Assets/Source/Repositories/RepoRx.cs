using Assets.Source.App;
using Assets.Source.Models;
using UnityEngine;

namespace Assets.Source.Repositories
{
    public class RepoRx : MonoBehaviour
    {
        private GameStateRepository _GameStateRepository;
        public GameStateRepository GameStateRepository
        {
            get
            {
                if(_GameStateRepository == null)
                {
                    _GameStateRepository = new GameStateRepository();
                }

                return _GameStateRepository;
            }
        }


        private JesterStateRepository _JesterStateRepository;
        public JesterStateRepository JesterStateRepository
        {
            get
            {
                if (_JesterStateRepository == null)
                {
                    _JesterStateRepository = new JesterStateRepository();
                }

                return _JesterStateRepository;
            }
        }

        private PlayerProfileRepository _PlayerProfileRepository;
        public PlayerProfileRepository PlayerProfileRepository
        {
            get
            {
                CreatePlayerProfileRepository();
                return _PlayerProfileRepository;
            }
        }

        private UserSettingsRepository _UserSettingsRepository;
        public UserSettingsRepository UserSettingsRepository
        {
            get
            {
                if (_UserSettingsRepository == null)
                {
                    _UserSettingsRepository = new UserSettingsRepository();
                }

                return _UserSettingsRepository;
            }
        }

        private void CreatePlayerProfileRepository()
        {
            if (_PlayerProfileRepository == null)
            {
                _PlayerProfileRepository = new PlayerProfileRepository(new FileDataStorage<PlayerProfile>("profile.save"));
            }
        }

        private void Awake()
        {
            CreatePlayerProfileRepository();
        }
    }
}
