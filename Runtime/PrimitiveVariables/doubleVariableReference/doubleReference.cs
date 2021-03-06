
using System;
using UnityEngine;


namespace VariableReference
{
    [Serializable]
    public class doubleReference : VariableReference<double, doubleScriptableObjectVariable, doubleMonoBehaviourVariable>
    {
        public doubleReference() : base() { }
        public doubleReference(double value) : base(value) { }
        public doubleReference(doubleScriptableObjectVariable newVariable) : base(newVariable) { }
        public doubleReference(doubleMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
