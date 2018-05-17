using Assets.Source.Config;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Config
{    
    public class JesterSoundEffectsConfigEditor
    {
        private static string GetSavePath()
        {
            return EditorUtility.SaveFilePanelInProject("New sound effect config", "JesterSoundEffectsConfig", "asset", "");
        }

        [MenuItem("Assets/Create/Config/Jester SoundEffects Config")]
        public static void CreateDatabase()
        {
            string assetPath = GetSavePath();
            JesterSoundEffectsConfig asset = ScriptableObject.CreateInstance("JesterSoundEffectsConfig") as JesterSoundEffectsConfig;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}
