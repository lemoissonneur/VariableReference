
using System;
using UnityEngine;


namespace VariableReference
{
	[Serializable, CreateAssetMenu(menuName = "Variables/string Variable", order = 1)]
    public class stringScriptableObjectVariable : ScriptableObjectVariable<string> { }
}
