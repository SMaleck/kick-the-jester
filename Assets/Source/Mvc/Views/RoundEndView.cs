using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using Zenject;

namespace Assets.Source.Mvc.Views
{
    public class RoundEndView : ClosableView
    {
        public ReactiveCommand OnRetryClicked = new ReactiveCommand();
        public ReactiveCommand OnShopClicked = new ReactiveCommand();


        public override void Setup()
        {
            base.Setup();
        }
    }
}
