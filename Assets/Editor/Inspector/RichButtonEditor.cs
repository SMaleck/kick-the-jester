using Assets.Source.Mvc.ServiceControllers;
using Assets.Source.Util.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Assets.Editor.Inspector
{
    [CustomEditor(typeof(RichButton))]
    public class RichButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            RichButton targetButton = (RichButton)target;
            targetButton.AudioEventType = (ButtonAudioEvent)EditorGUILayout.EnumPopup("Button Sound", targetButton.AudioEventType);
            
            base.OnInspectorGUI();
        }
    }
}
