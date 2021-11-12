using UnityEngine;
using UnityEditor;
using System.IO;


namespace CobayeStudio.VariableReference
{
    [CustomEditor(typeof(CustomVariableReferenceCreator), true)]
    public class CustomVariableReferenceCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedObject creatorObject = new SerializedObject(target);
            SerializedProperty createAtCurrentPathProperty = creatorObject.FindProperty(nameof(CustomVariableReferenceCreator.CreateAtCurrentPath));
            SerializedProperty targetPathProperty = creatorObject.FindProperty(nameof(CustomVariableReferenceCreator.TargetFolder));
            SerializedProperty typeNameProperty = creatorObject.FindProperty(nameof(CustomVariableReferenceCreator.TypeName));

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical();

            GUILayout.Label("Create a custom VariableReference with your own serializable type.", GUILayout.ExpandWidth(false));
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(createAtCurrentPathProperty);

            if (!createAtCurrentPathProperty.boolValue)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                EditorGUILayout.PrefixLabel(new GUIContent(targetPathProperty.name, targetPathProperty.tooltip));

                GUILayout.Label("Assets/", GUILayout.ExpandWidth(false));

                targetPathProperty.stringValue = EditorGUILayout.TextField(targetPathProperty.stringValue);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.PropertyField(typeNameProperty);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (EditorGUI.EndChangeCheck())
                creatorObject.ApplyModifiedProperties();

            GUILayout.Label("Result :", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            CustomVariableReferenceCreator creator = target as CustomVariableReferenceCreator;

            if (creator.TargetParentFolderExist())
            {
                int indentLevel = DrawFolder(creator.GetNewFolderPath());

                if (creator.TypeName != null)
                {
                    DrawIndented(CustomVariableReferenceCreator.RefFileName.Replace("#CUSTOMTYPE#", creator.TypeName), indentLevel);
                    DrawIndented(CustomVariableReferenceCreator.SOFileName.Replace("#CUSTOMTYPE#", creator.TypeName), indentLevel);
                    DrawIndented(CustomVariableReferenceCreator.MonoFileName.Replace("#CUSTOMTYPE#", creator.TypeName), indentLevel);
                }
            }
            else
                GUILayout.Label("target folder does not exist !");


            if (GUILayout.Button("Create"))
            {
                creator.Create();
            }

            EditorGUILayout.EndVertical();
        }

        private const int indentSize = 10;

        private void DrawIndented(string label, int level)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight));

            for (int i = 0; i < level; i++)
                EditorGUILayout.Space(indentSize, false);

            level++;

            GUILayout.Label(label);

            EditorGUILayout.EndHorizontal();
        }

        private int DrawFolder(string completePath)
        {
            int indentLevel = 0;

            foreach (string s in completePath.Split('/'))
            {
                DrawIndented(s + "/", indentLevel++);
            }

            return indentLevel;
        }
    }
}