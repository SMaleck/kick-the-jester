using Assets.Source.Behaviours;
using Assets.Source.Behaviours.GameLogic;
using Assets.Source.Behaviours.Jester;
using Assets.Source.Behaviours.MainCamera;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Cache
    {
        public static ICamera MainCamera
        {
            get { return Camera.main.GetComponent<ICamera>(); }
        }


        private static GameLogicContainer _gameLogic;
        public static GameLogicContainer GameLogic
        {
            get
            {
                if (_gameLogic == null)
                {
                    _gameLogic = GetComponentFrom<GameLogicContainer>(Constants.GO_GAME_LOGIC);
                }

                return _gameLogic;
            }
        }


        private static JesterContainer _jester;
        public static JesterContainer Jester
        {
            get
            {
                if (_jester == null)
                {
                    _jester = GetComponentFrom<JesterContainer>(Constants.GO_JESTER);
                }

                return _jester;
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