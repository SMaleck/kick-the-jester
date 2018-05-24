using Assets.Source.Config;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Config
{
    public class SpawningLanesConfigEditor
    {
        private static string GetSavePath()
        {
            return EditorUtility.SaveFilePanelInProject("New spawning lanes config", "SpawningLanesConfig", "asset", "", "Assets/Configs");
        }

        [MenuItem("Assets/Create/Config/Spawning Lanes Config")]
        public static void CreateDatabase()
        {
            string assetPath = GetSavePath();
            SpawningLanesConfig asset = ScriptableObject.CreateInstance("SpawningLanesConfig") as SpawningLanesConfig;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}
