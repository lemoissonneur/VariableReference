using TMPro;
using UnityEngine;


namespace CobayeStudio.VariableReference.UIText
{
    /// <summary>
    /// Extention of Unity TMP.Text component to automatically write the content of a MonoBehaviourVariable or ScriptableObject variable in a Text
    /// </summary>
    [AddComponentMenu("Cobaye Studio/UI/VariableReference TextMeshPro")]
    public class UIVariableReferenceTextMeshPro : TextMeshProUGUI
    {
        public VariableReference reference;

        private void Update()
        {
            if (reference.ReferenceType != VariableReference.ReferenceTypes.LOCAL)
                text = reference.ValueToString();
        }
    }
}
