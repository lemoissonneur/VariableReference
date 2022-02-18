
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    [Serializable]
    public class floatReference : VariableReference<float, floatScriptableObjectVariable, floatMonoBehaviourVariable>
    {
        public floatReference() : base() { }
        public floatReference(float value) : base(value) { }
        public floatReference(floatScriptableObjectVariable newVariable) : base(newVariable) { }
        public floatReference(floatMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
