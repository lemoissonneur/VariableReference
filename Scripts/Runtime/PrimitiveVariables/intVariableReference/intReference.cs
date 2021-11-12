
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    [Serializable]
    public class intReference : VariableReference<int, intScriptableObjectVariable, intMonoBehaviourVariable>
    {
        public intReference(int value) : base(value) { }
        public intReference(intScriptableObjectVariable newVariable) : base(newVariable) { }
        public intReference(intMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
