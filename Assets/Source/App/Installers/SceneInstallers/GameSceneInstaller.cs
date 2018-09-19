using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Source.Mvc.Models;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {


            Container.Bind<FlightStatsModel>().AsSingle().NonLazy();
        }
    }
}
