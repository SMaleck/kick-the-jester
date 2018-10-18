using Assets.Source.Mvc.Models;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Source.Entities
{
    public class UserInputEntity : AbstractMonoEntity, IPointerDownHandler
    {
        private UserInputModel _userInputModel;
        private GameStateModel _gameStateModel;

        [Inject]
        private void Inject(UserInputModel userInputModel, GameStateModel gameStateModel)
        {
            _userInputModel = userInputModel;
            _gameStateModel = gameStateModel;
        }

        public override void Initialize()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => CheckRawInput());
        }


        private void CheckRawInput()
        {
            var isPaused = _gameStateModel.IsPaused.Value;

            if (!isPaused && Input.GetButtonDown("Kick"))
            {
                _userInputModel.OnClickedAnywhere.Execute();
            }

            if (Input.GetButtonDown("Pause"))
            {
                _userInputModel.OnPause.Execute();
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {            
            switch (eventData.pointerId)
            {
                case -1:
                case 0:
                    if (!_gameStateModel.IsPaused.Value)
                    {
                        _userInputModel.OnClickedAnywhere.Execute();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
