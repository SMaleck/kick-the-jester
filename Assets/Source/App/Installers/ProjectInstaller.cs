using Zenject;

namespace Assets.Source.App.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MvcInstaller.Install(Container);
            ServiceInstaller.Install(Container);            
        }
    }
}
