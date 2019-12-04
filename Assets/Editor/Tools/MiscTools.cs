using Assets.Source.App;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Tools
{
    [InitializeOnLoad]
    public class MiscTools : ScriptableObject
    {
        private const string SavegameExtension = ".sav";

        /// <summary>
        /// Deletes the current savegame
        /// </summary>
        [MenuItem(Constants.UMenuRoot + "Delete current Savegames")]
        static void DeleteCurrentSavegame()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }

            var basePath = Application.persistentDataPath;
            if (!Directory.Exists(basePath))
            {
                return;
            }

            Directory.GetFiles(basePath)
                .Where(filePath => filePath.EndsWith(SavegameExtension))
                .ToList()
                .ForEach(File.Delete);
        }
    }
}
