
using System;
using UnityEngine;


namespace VariableReference
{
    [Serializable]
    public class Vector3Reference : VariableReference<Vector3, Vector3ScriptableObjectVariable, Vector3MonoBehaviourVariable>
    {
        public Vector3Reference() : base() { }
        public Vector3Reference(Vector3 value) : base(value) { }
        public Vector3Reference(Vector3ScriptableObjectVariable newVariable) : base(newVariable) { }
        public Vector3Reference(Vector3MonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
