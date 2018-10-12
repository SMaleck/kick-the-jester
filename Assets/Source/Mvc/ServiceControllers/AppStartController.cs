using Assets.Source.Services;

// ToDo Chnage Namespace
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
