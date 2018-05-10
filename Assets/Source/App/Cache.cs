﻿using Assets.Source.Behaviours.Jester;
using Assets.Source.GameLogic;
using Assets.Source.Repositories;
using Assets.Source.Service;
using System;
using UnityEngine;

namespace Assets.Source.App
{
    public static class Cache
    {
        private static Services services;
        public static Services Services
        {
            get
            {
                if (services == null)
                {
                    services = GetComponentFrom<Services>(Constants.GO_SERVICES);
                }

                return services;
            }
        }


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

        private static RepoRx _RepoRx;
        public static RepoRx RepoRx
        {
            get
            {
                if (_RepoRx == null)
                {
                    _RepoRx = Repositories.GetComponent<RepoRx>();
                }
                return _RepoRx;
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
                    _userControl = GetComponentFrom<UserControl>(Constants.GO_USER_CONTROL);
                }
                return _userControl;
            }
        }


        // ToDo Remove
        public static PlayerProfileRepository playerProfile
        {
            get
            {
                return RepoRx.PlayerProfileRepository;
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

        private static Camera _mainCamera;
        public static Camera mainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }

                return _mainCamera;
            }
        }


        private static float _cameraWidth;
        public static float cameraWidth
        {
            get
            {
                if(_cameraWidth <= 0)
                {                                        
                    _cameraWidth = cameraHeight * mainCamera.aspect;
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
                    _cameraHeight = 2f * mainCamera.orthographicSize;                    
                }

                return _cameraHeight;
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