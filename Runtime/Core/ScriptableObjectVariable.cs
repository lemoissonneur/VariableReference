using UnityEngine;


namespace VariableReference
{
    /// <summary>
    /// Base class for ScriptableObjectVariable
    /// </summary>
    public abstract class ScriptableObjectVariable : ScriptableObject
    {
        public abstract string ValueToString();
    }

    /// <summary>
    /// Base class with generics to inherit from when implementing variable reference for a specific type
    /// </summary>
    /// <typeparam name="T">Type of value to be referenced</typeparam>
    public class ScriptableObjectVariable<T> : ScriptableObjectVariable
    {
        [SerializeField]
        protected T value;
        public T Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Return Value.ToString()
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ValueToString() => value.ToString();

        public static implicit operator T(ScriptableObjectVariable<T> variable)
        {
            return variable.Value;
        }
    }
}