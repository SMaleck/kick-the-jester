using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Assets.Editor.Keyboard
{
    [InitializeOnLoad]
    public class HotKeys : ScriptableObject
    {
        private const string LAST_ACTIVE_SCENE = "kj.lastActiveScene";

        static HotKeys()
        {
            EditorApplication.playModeStateChanged += StateChanged;
        }

        /// <summary>
        /// Runs the game from the Default scene.
        /// </summary>
        [MenuItem("Tools/Run Game %L")]
        static void RunGameFromDefaultScene()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorPrefs.SetString(LAST_ACTIVE_SCENE, EditorSceneManager.GetActiveScene().path);

                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
                EditorApplication.isPlaying = true;
            }            
        }

        static void StateChanged(PlayModeStateChange state)
        {
            if (state.Equals(PlayModeStateChange.EnteredEditMode))
            {
                if (EditorPrefs.HasKey(LAST_ACTIVE_SCENE) &&
                    !EditorSceneManager.GetActiveScene().Equals(EditorPrefs.GetString(LAST_ACTIVE_SCENE)))
                {
                    EditorSceneManager.OpenScene(EditorPrefs.GetString(LAST_ACTIVE_SCENE));
                }
            }
        }
    }
}
