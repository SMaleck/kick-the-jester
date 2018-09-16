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
            //LinkButton linkButtonTarget = (LinkButton)target;
            //linkButtonTarget.Url = EditorGUILayout.TextField("Url", "Url");
            
            base.OnInspectorGUI();
        }
    }
}
