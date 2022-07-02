
using System;
using UnityEngine;


namespace VariableReference
{
	[Serializable, CreateAssetMenu(menuName = "Variables/Quaternion Variable", order = 1)]
    public class QuaternionScriptableObjectVariable : ScriptableObjectVariable<Quaternion> { }
}
