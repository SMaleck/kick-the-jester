using Assets.Source.Behaviours.Jester;
using Assets.Source.GameLogic;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Cache
    {
        #region GAME LOGIC Components

        // GameLogic is the GameObject which holds most global scripts
        private static GameObject _GameLogic;
        private static GameObject GameLogic
        {
            get
            {
                if (_GameLogic == null)
                {
                    _GameLogic = GameObject.Find(Constants.GO_GAME_LOGIC);
                }

                return _GameLogic;
            }
        }        

        private static RxState _rxState;
        public static RxState rxState
        {
            get
            {
                if (_rxState == null)
                {
                    _rxState = GameLogic.GetComponent<RxState>();
                }

                return _rxState;
            }
        }

        private static GameStateManager _gameStateManager;
        public static GameStateManager gameStateManager
        {
            get
            {
                if (_gameStateManager == null)
                {
                    _gameStateManager = GameLogic.GetComponent<GameStateManager>();
                }

                return _gameStateManager;
            }
        }

        private static CurrencyManager _currencyManager;
        public static CurrencyManager currencyManager
        {
            get
            {
                if (_currencyManager == null)
                {
                    _currencyManager = GameLogic.GetComponent<CurrencyManager>();
                }

                return _currencyManager;
            }
        }

        private static UserControl _userControl;
        public static UserControl userControl
        {
            get
            {
                if (_userControl == null)
                {
                    _userControl = GameLogic.GetComponent<UserControl>();
                }
                return _userControl;
            }
        }

        private static ScreenManager _screenManager;
        public static ScreenManager screenManager
        {
            get
            {
                if (_screenManager == null)
                {
                    _screenManager = GameLogic.GetComponent<ScreenManager>();
                }
                return _screenManager;
            }
        }

        private static PlayerProfile _playerProfile;
        public static PlayerProfile playerProfile
        {
            get
            {
                if (_playerProfile == null)
                {
                    _playerProfile = GetComponent<PlayerProfile>(Constants.GO_PLAYER_PROFILE);
                }
                return _playerProfile;
            }
        }

        #endregion


        private static float _cameraWidth;
        public static float cameraWidth
        {
            get
            {
                if(_cameraWidth <= 0)
                {                                        
                    _cameraWidth = cameraHeight * Camera.main.aspect;
                }

                return _cameraWidth;
            }
        }

        private static float _cameraHeight;
        public static float cameraHeight
        {
            get
            {
                if (_cameraHeight <= 0)
                {
                    _cameraHeight = 2f * Camera.main.orthographicSize;                    
                }

                return _cameraHeight;
            }
        }

        private static Jester _jester;
        public static Jester jester
        {
            get
            {
                if (_jester == null)
                {
                    _jester = GetComponent<Jester>(Constants.JESTER);
                }

                return _jester;
            }
        }


        /* ------------------------------------------------------------------------------------ */
        #region Utilities

        private static T GetComponent<T>(string gameObjectId)
        {
            T component = GameObject.Find(gameObjectId).GetComponent<T>();
            AssertNotNull(component);

            return component;
        }


        private static void AssertNotNull(object Obj)
        {
            if (Obj == null)
            {
                throw new NullReferenceException();
            }
        }

        #endregion
    }
}