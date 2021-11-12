
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference.Example
{
    [Serializable]
    public class ExampleSerializableClassReference : VariableReference<ExampleSerializableClass, ExampleSerializableClassScriptableObjectVariable, ExampleSerializableClassMonoBehaviourVariable>
    {
        public ExampleSerializableClassReference(ExampleSerializableClass value) : base(value) { }
        public ExampleSerializableClassReference(ExampleSerializableClassScriptableObjectVariable newVariable) : base(newVariable) { }
        public ExampleSerializableClassReference(ExampleSerializableClassMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
