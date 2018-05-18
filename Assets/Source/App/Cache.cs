using Assets.Source.Behaviours.Jester;
using Assets.Source.Behaviours.MainCamera;
using Assets.Source.GameLogic;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Cache
    {
        /* -------------------------------------------------------------------- */
        #region GAME OBJECTS

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

        private static GameObject _Repositories;
        private static GameObject Repositories
        {
            get
            {
                if (_Repositories == null)
                {
                    _Repositories = GameObject.Find(Constants.GO_REPOSITORIES);
                }

                return _Repositories;
            }
        }

        #endregion


        /* -------------------------------------------------------------------- */
        #region COMPONENTS

        private static GameStateMachine gameStateMachine;
        public static GameStateMachine GameStateMachine
        {
            get
            {
                if (gameStateMachine == null)
                {
                    gameStateMachine = GameLogic.GetComponent<GameStateMachine>();
                }

                return gameStateMachine;
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
                    _userControl = GetComponentFrom<UserControl>(Constants.GO_USER_CONTROL);
                }
                return _userControl;
            }
        }


        private static Jester _jester;
        public static Jester jester
        {
            get
            {
                if (_jester == null)
                {
                    _jester = GetComponentFrom<Jester>(Constants.GO_JESTER);
                }

                return _jester;
            }
        }

        #endregion
        
        public static ICamera MainCamera
        {
            get { return Camera.main.GetComponent<ICamera>(); }
        }

        
        /* ------------------------------------------------------------------------------------ */
        #region Utilities

        private static T GetComponentFrom<T>(string gameObjectId)
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