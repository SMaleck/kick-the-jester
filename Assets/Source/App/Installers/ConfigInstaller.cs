using Assets.Source.App.Configuration;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GameConfig GameConfig;
        [SerializeField] private DefaultSettingsConfig DefaultSettingsConfig;
        [SerializeField] private ViewPrefabConfig ViewPrefabConfig;


        public override void InstallBindings()
        {            
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstances(DefaultSettingsConfig);
            Container.BindInstances(ViewPrefabConfig);
        }
    }
}