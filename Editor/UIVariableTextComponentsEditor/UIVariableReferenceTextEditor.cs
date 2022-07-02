using UnityEditor;
using UnityEditor.UI;


namespace CobayeStudio.VariableReference.UIText
{
    [CustomEditor(typeof(UIVariableReferenceText), false)]
    public class UIVariableReferenceTextEditor : TextEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceText.reference)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceText.prefix)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceText.suffix)));

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
