
using System;
using UnityEngine;


namespace VariableReference.Sample
{
	[Serializable, CreateAssetMenu(menuName = "Variables/ExampleSerializableClass Variable", order = 1)]
    public class ExampleSerializableClassScriptableObjectVariable : ScriptableObjectVariable<ExampleSerializableClass> { }
}
