using Assets.Source.App.Configuration;
using Assets.Source.Entities.Jester.Config;
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
        [SerializeField] private JesterSpriteEffectsConfig JesterSpriteEffectsConfig;
        [SerializeField] private JesterSoundEffectsConfig _jesterSoundEffectsConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(GameConfig.DefaultSettingsConfig);
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstance(GameConfig.AudioConfig);
            Container.BindInstance(GameConfig.BalancingConfig);
            Container.BindInstance(GameConfig.BootConfig);

            Container.BindInstance(AudioEventConfig);

            Container.BindInstance(JesterSpriteEffectsConfig);
            Container.BindInstances(_jesterSoundEffectsConfig);
        }
    }
}