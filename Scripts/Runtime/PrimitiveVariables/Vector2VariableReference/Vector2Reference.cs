
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    [Serializable]
    public class Vector2Reference : VariableReference<Vector2, Vector2ScriptableObjectVariable, Vector2MonoBehaviourVariable>
    {
        public Vector2Reference(Vector2 value) : base(value) { }
        public Vector2Reference(Vector2ScriptableObjectVariable newVariable) : base(newVariable) { }
        public Vector2Reference(Vector2MonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
