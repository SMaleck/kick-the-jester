using Assets.Source.Mvc.Models;
using Assets.Source.Mvc.Views;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Source.Mvc.Controllers
{
    public class UserInputController : AbstractController
    {
        private readonly UserInputView _view;
        private readonly UserInputModel _userInputModel;
        private readonly GameStateModel _gameStateModel;


        public UserInputController(UserInputView view, UserInputModel userInputModel, GameStateModel gameStateModel)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _userInputModel = userInputModel;
            _gameStateModel = gameStateModel;

            _view.OnClickedAnywhere
                .Where(_ => !_gameStateModel.IsPaused.Value)
                .Subscribe(_ => _userInputModel.OnClickedAnywhere.Execute())
                .AddTo(Disposer);

            Observable.EveryUpdate()
                .Subscribe(_ => CheckRawInput());
        }


        private void CheckRawInput()
        {
            var isPaused = _gameStateModel.IsPaused.Value;

            if (!isPaused && (Input.GetButtonDown("Kick") || Input.GetMouseButtonDown(0)))
            {
                _userInputModel.OnClickedAnywhere.Execute();
            }

            if (Input.GetButtonDown("Pause"))
            {
                _userInputModel.OnPause.Execute();
            }
        }
    }
}
