using Assets.Source.Entities.Jester.Config;
using UnityEngine;
using Zenject;

namespace Assets.Source.App.Installers.SceneInstallers
{
    [CreateAssetMenu(fileName = "GameSceneConfigInstaller", menuName = "Installers/GameSceneConfigInstaller")]
    public class GameSceneScriptableObjectInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private JesterSpriteEffectsConfig _jesterSpriteEffectsConfig;
        [SerializeField] private JesterSoundEffectsConfig _jesterSoundEffectsConfig;

        public override void InstallBindings()
        {
            Container.BindInstances(_jesterSpriteEffectsConfig);
            Container.BindInstances(_jesterSoundEffectsConfig);
        }
    }
}
