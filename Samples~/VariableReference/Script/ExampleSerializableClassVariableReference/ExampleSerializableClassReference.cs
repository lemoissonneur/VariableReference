
using System;
using UnityEngine;


namespace VariableReference.Sample
{
    [Serializable]
    public class ExampleSerializableClassReference : VariableReference<ExampleSerializableClass, ExampleSerializableClassScriptableObjectVariable, ExampleSerializableClassMonoBehaviourVariable>
    {
        public ExampleSerializableClassReference() : base() { }
        public ExampleSerializableClassReference(ExampleSerializableClass value) : base(value) { }
        public ExampleSerializableClassReference(ExampleSerializableClassScriptableObjectVariable newVariable) : base(newVariable) { }
        public ExampleSerializableClassReference(ExampleSerializableClassMonoBehaviourVariable newVariable) : base(newVariable) { }
    }
}
