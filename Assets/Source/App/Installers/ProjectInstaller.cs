using Zenject;

namespace Assets.Source.App.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ServiceInstaller.Install(Container);
            MvcInstaller.Install(Container);            
        }
    }
}
