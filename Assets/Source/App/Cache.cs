using Assets.Source.Entities;
using Assets.Source.GameLogic;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Cache
    {
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

        private static GameStateManager _gameStateManager;
        public static GameStateManager gameStateManager
        {
            get
            {
                if (_gameStateManager == null)
                {
                    _gameStateManager = GetComponent<GameStateManager>(Constants.GAMESTATE_MANAGER);
                }
                
                return _gameStateManager;
            }
        }
        
        private static RxState _rxState;
        public static RxState rxState
        {
            get
            {
                if (_rxState == null)
                {
                    _rxState = GetComponent<RxState>(Constants.GAMESTATE_MANAGER);
                }

                return _rxState;
            }
        }

        private static UserControl _userControl;
        public static UserControl userControl
        {
            get
            {
                if (_userControl == null)
                {
                    _userControl = GetComponent<UserControl>(Constants.USER_CONTROL);
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
                    _screenManager = GetComponent<ScreenManager>(Constants.SCREEN_MANAGER);
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
                    _playerProfile = GetComponent<PlayerProfile>(Constants.PLAYER_PROFILE);
                }
                return _playerProfile;
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