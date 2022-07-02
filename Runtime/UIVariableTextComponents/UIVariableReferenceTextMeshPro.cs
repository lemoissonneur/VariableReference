using TMPro;
using UnityEngine;


namespace VariableReference.UIText
{
    /// <summary>
    /// Extention of Unity TMP.Text component to automatically write the content of a MonoBehaviourVariable or ScriptableObject variable in a Text
    /// </summary>
    [AddComponentMenu("UI/VariableReference TextMeshPro")]
    public class UIVariableReferenceTextMeshPro : TextMeshProUGUI
    {
        public VariableReference reference;
        public string prefix;
        public string suffix;

        private void Update()
        {
            text = prefix + reference.ValueToString() + suffix;
        }
    }
}
