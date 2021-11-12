
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    [Serializable]
    public class stringReference : VariableReference<string, stringScriptableObjectVariable, stringMonoBehaviourVariable>
    {
        public stringReference(string value) : base(value) { }
        public stringReference(stringScriptableObjectVariable newVariable) : base(newVariable) { }
        public stringReference(stringMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
