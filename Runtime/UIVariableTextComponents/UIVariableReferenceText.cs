using UnityEngine;
using UnityEngine.UI;


namespace VariableReference.UIText
{
    /// <summary>
    /// Extention of Unity Text component to automatically write the content of a MonoBehaviourVariable or ScriptableObject variable in a Text
    /// </summary>
    [AddComponentMenu("UI/VariableReference Text")]
    public class UIVariableReferenceText : Text
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
