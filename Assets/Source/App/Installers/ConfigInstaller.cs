using Assets.Source.App.Configuration;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Data;
using Assets.Source.Services.Particles;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = Constants.PROJECT_MENU_ROOT + "/Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GameConfig GameConfig;
        [SerializeField] private AudioEventConfig AudioEventConfig;

        [Header("Jester Configs")]
        [SerializeField] private JesterSpriteEffectsConfig _jesterSpriteEffectsConfig;
        [SerializeField] private JesterSoundEffectsConfig _jesterSoundEffectsConfig;
        [SerializeField] private UpgradeTreeConfig _upgradeTreeConfig;

        [Header("View Prefabs")]
        [SerializeField] private ViewPrefabConfig _viewPrefabConfig;

        [Header("Pooling Configs")]
        [SerializeField] private ParticleEffectConfig _particleEffectConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(GameConfig.DefaultSettingsConfig);
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstance(GameConfig.AudioConfig);
            Container.BindInstance(GameConfig.BalancingConfig);
            Container.BindInstance(GameConfig.BootConfig);
            Container.BindInstance(GameConfig.ShootConfig);

            Container.BindInstance(AudioEventConfig);

            Container.BindInstance(_jesterSpriteEffectsConfig);
            Container.BindInstances(_jesterSoundEffectsConfig);
            Container.BindInstances(_upgradeTreeConfig);

            Container.BindInstances(_viewPrefabConfig);

            Container.BindInstances(_particleEffectConfig);
        }
    }
}