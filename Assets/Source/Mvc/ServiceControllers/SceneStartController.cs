using Assets.Source.Services;
using Zenject;

namespace Assets.Source.Mvc.ServiceControllers
{
    public class SceneStartController : IInitializable
    {
        private readonly SceneTransitionService _sceneTransitionService;

        public SceneStartController(SceneTransitionService sceneTransitionService)
        {
            _sceneTransitionService = sceneTransitionService;
        }

        public void Initialize()
        {
            _sceneTransitionService.PublishOnSceneInitComplete();
        }
    }
}
