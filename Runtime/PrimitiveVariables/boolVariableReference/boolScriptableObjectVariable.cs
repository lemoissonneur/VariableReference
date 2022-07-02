
using System;
using UnityEngine;


namespace VariableReference
{
	[Serializable, CreateAssetMenu(menuName = "Variables/bool Variable", order = 1)]
    public class boolScriptableObjectVariable : ScriptableObjectVariable<bool> { }
}
