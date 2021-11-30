using UnityEditor;
using TMPro.EditorUtilities;


namespace CobayeStudio.VariableReference.UIText
{
    [CustomEditor(typeof(UIVariableReferenceTextMeshPro), false)]
    public class UIVariableReferenceTextMeshProEditor : TMP_EditorPanelUI
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceTextMeshPro.reference)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceTextMeshPro.prefix)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(UIVariableReferenceTextMeshPro.suffix)));

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}
