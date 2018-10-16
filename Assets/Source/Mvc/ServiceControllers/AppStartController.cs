using Assets.Source.Services;

namespace Assets.Source.Mvc.ServiceControllers
{
    public class AppStartController
    {
        public AppStartController(SceneTransitionService sceneTransitionService)
        {
            sceneTransitionService.ToTitle();
        }
    }
}
