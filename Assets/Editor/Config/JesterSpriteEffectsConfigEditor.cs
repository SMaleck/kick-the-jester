using Assets.Source.Config;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Config
{    
    public class JesterSpriteEffectsConfigEditor
    {
        private static string GetSavePath()
        {
            return EditorUtility.SaveFilePanelInProject("New sprite effect config", "JesterSpriteEffectsConfig", "asset", "");
        }

        [MenuItem("Assets/Create/Config/Jester SpriteEffects Config")]
        public static void CreateDatabase()
        {
            string assetPath = GetSavePath();
            JesterSpriteEffectsConfig asset = ScriptableObject.CreateInstance("JesterSpriteEffectsConfig") as JesterSpriteEffectsConfig;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}
