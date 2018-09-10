using Assets.Source.Services;

namespace Assets.Source.App
{
    public class AppStartController
    {
        public AppStartController(SceneTransitionService sceneTransitionService)
        {
            sceneTransitionService.ToTitle();
        }
    }
}
