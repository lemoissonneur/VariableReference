
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference.Example
{
	[Serializable, CreateAssetMenu(menuName = "Cobaye Studio/Variables/ExampleSerializableClass Variable", order = 1)]
    public class ExampleSerializableClassScriptableObjectVariable : ScriptableObjectVariable<ExampleSerializableClass> { }
}
