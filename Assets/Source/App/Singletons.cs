using Assets.Source.Entities;
using Assets.Source.GameLogic;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Singletons
    {
        private static Jester _jester;
        public static Jester jester
        {
            get
            {
                return SafeGet(_jester, Constants.JESTER);
            }
        }

        private static GameStateManager _gameStateManager;
        public static GameStateManager gameStateManager
        {
            get
            {
                return SafeGet(_gameStateManager, Constants.GAMESTATE_MANAGER);
            }
        }


        private static UserControl _userControl;
        public static UserControl userControl
        {
            get
            {
                return SafeGet(_userControl, Constants.USER_CONTROL);
            }
        }

        private static ScreenManager _screenManager;
        public static ScreenManager screenManager
        {
            get
            {
                return SafeGet(_screenManager, Constants.SCREEN_MANAGER);
            }
        }

        private static PlayerProfile _playerProfile;
        public static PlayerProfile playerProfile
        {
            get { return SafeGet(_playerProfile, Constants.PLAYER_PROFILE); }
        }


        /* ------------------------------------------------------------------------------------ */
        #region Utilities

        private static T SafeGet<T>(T privateReference, string gameObjectid) where T: class
        {
            if(privateReference == null)
            {
                privateReference = GetSingletonGameObject<T>(gameObjectid);
            }

            return privateReference;
        }


        private static T GetSingletonGameObject<T>(string gameObjectId)
        {
            T singleton = GameObject.Find(gameObjectId).GetComponent<T>();
            AssertNotNull(singleton);

            return singleton;
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