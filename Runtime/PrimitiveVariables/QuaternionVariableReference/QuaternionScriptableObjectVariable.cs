
using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
	[Serializable, CreateAssetMenu(menuName = "Cobaye Studio/Variables/Quaternion Variable", order = 1)]
    public class QuaternionScriptableObjectVariable : ScriptableObjectVariable<Quaternion> { }
}
