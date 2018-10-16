﻿using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Mvc.Views
{
    public class UserInputView : AbstractView
    {
        [SerializeField] private List<Button> _screenSpaceButtons;

        public ReactiveCommand OnClickedAnywhere = new ReactiveCommand();

        
        public override void Setup()
        {
            _screenSpaceButtons.ForEach(button => 
            {
                button.OnClickAsObservable()
                    .Subscribe(_ => OnClickedAnywhere.Execute())
                    .AddTo(this);
            });
        }
    }
}
