using Assets.Source.App.Configuration;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GameConfig GameConfig;        
        [SerializeField] private ViewPrefabConfig ViewPrefabConfig;


        public override void InstallBindings()
        {
            Container.BindInstances(GameConfig.DeaultSettings);
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstance(GameConfig.AudioConfig);
            Container.BindInstance(GameConfig.BalancingConfig);

            Container.BindInstances(ViewPrefabConfig);
        }
    }
}