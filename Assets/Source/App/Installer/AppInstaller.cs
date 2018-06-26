using UnityEngine;
using Zenject;

public class AppInstaller : MonoInstaller<AppInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Injected.");
        Container.Bind<Test>().AsSingle().NonLazy();
    }
}