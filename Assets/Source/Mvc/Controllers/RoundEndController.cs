using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Mvc.Views;
using Assets.Source.Services;
using UniRx;

namespace Assets.Source.Mvc.Controllers
{
    public class RoundEndController : ClosableController
    {
        private readonly RoundEndView _view;
        private readonly SceneTransitionService _sceneTransitionService;

        public RoundEndController(RoundEndView view, SceneTransitionService sceneTransitionService)
            : base(view)
        {
            _view = view;
            _view.Initialize();

            _view.OnRetryClicked
                .Subscribe(_ => OnRetryClicked())
                .AddTo(Disposer);

            _view.OnShopClicked
                .Subscribe(_ => OnShopClicked())
                .AddTo(Disposer);

            _sceneTransitionService = sceneTransitionService;
        }

        private void OnRetryClicked()
        {
            _sceneTransitionService.ToGame();
        }

        private void OnShopClicked() { }
    }
}
