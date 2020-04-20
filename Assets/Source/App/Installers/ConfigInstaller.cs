using Assets.Source.App.Configuration;
using Assets.Source.Entities.Jester.Config;
using Assets.Source.Features.Achievements.Data;
using Assets.Source.Features.PickupItems.Data;
using Assets.Source.Features.Upgrades.Data;
using Assets.Source.Mvc.Data;
using Assets.Source.Services.Audio;
using Assets.Source.Services.Particles;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigInstaller), menuName = Constants.UMenuInstallers + nameof(ConfigInstaller))]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private GameConfig GameConfig;
        [SerializeField] private AchievementsConfig _achievementsConfig;
        [SerializeField] private AchievementsIconConfig _achievementsIconConfig;
        [SerializeField] private WorldObjectSpawnConfig _worldObjectSpawnConfig;

        [Header("Jester Configs")]
        [SerializeField] private JesterSpriteEffectsConfig _jesterSpriteEffectsConfig;
        [SerializeField] private UpgradeTreeConfig _upgradeTreeConfig;

        [Header("View Prefabs")]
        [SerializeField] private ViewPrefabConfig _viewPrefabConfig;
        [SerializeField] private ViewUtilConfig _viewUtilConfig;

        [Header("Pooling Configs")]
        [SerializeField] private ParticleEffectConfig _particleEffectConfig;
        [SerializeField] private AudioConfig _audioConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(GameConfig.DefaultSettingsConfig);
            Container.BindInstance(GameConfig.CameraConfig);
            Container.BindInstance(GameConfig.BalancingConfig);
            Container.BindInstance(GameConfig.BootConfig);
            Container.BindInstance(GameConfig.ShootConfig);

            Container.BindInstance(_jesterSpriteEffectsConfig);
            Container.BindInstance(_upgradeTreeConfig);

            Container.BindInstance(_viewPrefabConfig);
            Container.BindInstance(_viewUtilConfig);

            Container.BindInstance(_particleEffectConfig);
            Container.BindInstance(_audioConfig);

            Container.BindInstance(_achievementsIconConfig);

            Container.Bind<IAchievementData>()
                .FromInstance(_achievementsConfig);

            Container.Bind<IWorldObjectSpawnData>()
                .FromInstance(_worldObjectSpawnConfig);
        }
    }
}