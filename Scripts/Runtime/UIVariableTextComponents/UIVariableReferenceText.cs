using UnityEngine;
using UnityEngine.UI;


namespace CobayeStudio.VariableReference.UIText
{
    /// <summary>
    /// Extention of Unity Text component to automatically write the content of a MonoBehaviourVariable or ScriptableObject variable in a Text
    /// </summary>
    [AddComponentMenu("Cobaye Studio/UI/VariableReference Text")]
    public class UIVariableReferenceText : Text
    {
        public VariableReference reference;

        private void Update()
        {
            if(reference.ReferenceType != VariableReference.ReferenceTypes.LOCAL)
                text = reference.ValueToString();
        }
    }
}
