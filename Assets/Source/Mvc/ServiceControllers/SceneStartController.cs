using Assets.Source.Services;
using Assets.Source.Util;

namespace Assets.Source.Mvc.ServiceControllers
{
    public class SceneStartController : AbstractDisposable
    {
        public SceneStartController(SceneTransitionService sceneTransitionService)
        {
            sceneTransitionService.PublishOnSceneInitComplete();
        }
    }
}
