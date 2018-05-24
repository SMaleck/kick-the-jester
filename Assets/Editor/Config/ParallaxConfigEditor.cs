using Assets.Source.Config;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Config
{
    public class ParallaxConfigEditor
    {
        private static string GetSavePath()
        {
            return EditorUtility.SaveFilePanelInProject("New Parallax Config", "ParallaxConfig", "asset", "");
        }

        [MenuItem("Assets/Create/Config/Parallax Config")]
        public static void CreateDatabase()
        {
            string assetPath = GetSavePath();
            ParallaxConfig asset = ScriptableObject.CreateInstance("ParallaxConfig") as ParallaxConfig;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}
