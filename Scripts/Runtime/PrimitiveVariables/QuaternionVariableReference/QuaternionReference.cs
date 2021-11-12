
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    [Serializable]
    public class QuaternionReference : VariableReference<Quaternion, QuaternionScriptableObjectVariable, QuaternionMonoBehaviourVariable>
    {
        public QuaternionReference(Quaternion value) : base(value) { }
        public QuaternionReference(QuaternionScriptableObjectVariable newVariable) : base(newVariable) { }
        public QuaternionReference(QuaternionMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
