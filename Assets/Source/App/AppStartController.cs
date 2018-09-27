using Assets.Source.Services;

namespace Assets.Source.App
{
    public class AppStartController
    {
        public AppStartController(Assets.Source.Services.SceneTransitionService sceneTransitionService)
        {
            sceneTransitionService.ToTitle();
        }
    }
}
