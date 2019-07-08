using Assets.Source.App.Configuration;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Features.Upgrades.Data;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GameConfig GameConfig;
        [SerializeField] private AudioEventConfig AudioEventConfig;

        [Header("Jester Configs")]
        [SerializeField] private JesterSpriteEffectsConfig _jesterSpriteEffectsConfig;
        [SerializeField] private JesterSoundEffectsConfig _jesterSoundEffectsConfig;
        [SerializeField] private UpgradeTreeConfig _upgradeTreeConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(GameConfig.DefaultSettingsConfig);
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstance(GameConfig.AudioConfig);
            Container.BindInstance(GameConfig.BalancingConfig);
            Container.BindInstance(GameConfig.BootConfig);

            Container.BindInstance(AudioEventConfig);

            Container.BindInstance(_jesterSpriteEffectsConfig);
            Container.BindInstances(_jesterSoundEffectsConfig);
            Container.BindInstances(_upgradeTreeConfig);
        }
    }
}