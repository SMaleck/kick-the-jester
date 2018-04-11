using Assets.Source.GameLogic;
using Assets.Source.UI;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Singletons
    {
        private static UIManager _uiManager;
        public static UIManager uiManager
        {
            get
            {
                return SafeGet(_uiManager, Constants.UI_CANVAS);
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