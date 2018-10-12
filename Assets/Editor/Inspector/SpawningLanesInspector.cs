using System;
using System.Reflection;
using Assets.Source.Entities.Items.Config;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Inspector
{
    [Flags]
    public enum EditorListOption {
        None = 0,
        ListSize = 1,
        ListLabel = 2,
        Buttons = 4,
        Default = ListLabel,
        All = Default | Buttons
    }

    [CustomEditor(typeof(SpawningLanesConfig))]
    public class SpawningLanesInspector : UnityEditor.Editor
    {
        private static GUIContent moveButtonContent = new GUIContent("\u21b4", "move down"),
                                  duplicateButtonContent = new GUIContent("+", "duplicate"),
                                  deleteButtonContent = new GUIContent("-", "delete"),
                                  addButtonContent = new GUIContent("+", "add element");
        private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

        public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
        {
            bool showListLabel = (options & EditorListOption.ListLabel) != 0,
                 showListSize = (options & EditorListOption.ListSize) != 0;

            if (showListLabel)
            {
                EditorGUILayout.PropertyField(list);
                EditorGUI.indentLevel += 1;
            }
            
            if (!showListLabel || list.isExpanded)
            {
                if (showListSize) EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
                ShowElements(list, options);
            }
            if (showListLabel) EditorGUI.indentLevel -= 1;
        }

        private static void ShowElements(SerializedProperty list, EditorListOption options)
        {
            bool showButtons = (options & EditorListOption.Buttons) != 0;

            for (int i = 0; i < list.arraySize; i++)
            {
                if (showButtons) EditorGUILayout.BeginHorizontal();

                SerializedProperty property = list.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(property, true);

                if (showButtons)
                {
                    ShowButtons(list, i);
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (showButtons && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
            {
                list.arraySize += 1;
            }
        }

        private static void ShowButtons(SerializedProperty list, int index)
        {
            if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
            {
                list.MoveArrayElement(index, index + 1);
            }
            if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
            {
                list.InsertArrayElementAtIndex(index);
            }
            if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
            {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(index);
                if (list.arraySize == oldSize)
                {
                    list.DeleteArrayElementAtIndex(index);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            FieldInfo[] properties = typeof(SpawningLanesConfig).GetFields(BindingFlags.Instance | BindingFlags.Public);

            for (var i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name.Equals("SpawningLanes"))
                {
                    Show(serializedObject.FindProperty(properties[i].Name), EditorListOption.All);
                } else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(properties[i].Name));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
