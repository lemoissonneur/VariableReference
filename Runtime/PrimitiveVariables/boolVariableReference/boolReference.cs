
using System;
using UnityEngine;


namespace VariableReference
{
    [Serializable]
    public class boolReference : VariableReference<bool, boolScriptableObjectVariable, boolMonoBehaviourVariable>
    {
        public boolReference() : base() { }
        public boolReference(bool value) : base(value) { }
        public boolReference(boolScriptableObjectVariable newVariable) : base(newVariable) { }
        public boolReference(boolMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
