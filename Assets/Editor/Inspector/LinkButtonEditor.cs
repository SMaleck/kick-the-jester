using Assets.Source.Util.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Assets.Editor.Inspector
{
    [CustomEditor(typeof(LinkButton))]
    public class LinkButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            LinkButton targetButton = (LinkButton)target;
            targetButton.Url = (string)EditorGUILayout.TextField("Url", targetButton.Url);

            base.OnInspectorGUI();
        }
    }
}
