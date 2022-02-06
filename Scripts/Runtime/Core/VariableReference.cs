using System;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    /// <summary>
    /// Base class for all variables references. able to take any kind of SOVariable or MonoVariable for UIText elements 
    /// </summary>
    [Serializable]
    public class VariableReference
    {
        /// <summary>
        /// Variable reference types
        /// </summary>
        public enum ReferenceTypes { LOCAL, SCRIPTABLEOBJECTVARIABLE, MONOBEHAVIOURVARIABLE }

        /// <summary>
        /// Current type of reference (local, ScriptableObject or MonoBehaviour)
        /// </summary>
        [SerializeField] protected ReferenceTypes referenceType = ReferenceTypes.LOCAL;
        /// <summary>
        /// Current type of reference (local, ScriptableObject or MonoBehaviour)
        /// </summary>
        public ReferenceTypes ReferenceType => referenceType;

        [SerializeField] protected ScriptableObjectVariable GenericScriptableObjectValue;
        [SerializeField] protected MonoBehaviourVariable GenericMonoBehaviourValue;

        /// <summary>
        /// Get the result of ToString() of the current value
        /// </summary>
        /// <returns></returns>
        public virtual string ValueToString()
        {
            switch(referenceType)
            {
                default: return default;
                case ReferenceTypes.SCRIPTABLEOBJECTVARIABLE: return GenericScriptableObjectValue?.ValueToString();
                case ReferenceTypes.MONOBEHAVIOURVARIABLE: return GenericMonoBehaviourValue?.ValueToString();
            }
        }
    }

    /// <summary>
    /// Base class with generics to inherit from when implementing variable reference for a specific type
    /// </summary>
    /// <typeparam name="T">Type of value to be referenced</typeparam>
    /// <typeparam name="S">Implemented ScriptableObjectVariable of type T</typeparam>
    /// <typeparam name="M">Implemented MonoBehaviourVariable of type T</typeparam>
    [Serializable]
    public class VariableReference<T, S, M> : VariableReference, ISerializationCallbackReceiver
        where S : ScriptableObjectVariable<T>
        where M : MonoBehaviourVariable<T>
    {
        [SerializeField] protected T LocalValue;
        [SerializeField] protected S ScriptableObjectValue;
        [SerializeField] protected M MonoBehaviourValue;

        /// <summary>
        /// Empty constructor with default local value
        /// </summary>
        public VariableReference()
        {
            LocalValue = default;
            referenceType = ReferenceTypes.LOCAL;
            SetLocalValue();
        }

        /// <summary>
        /// Constructor with local value ( ReferenceType = ReferenceTypes.LOCAL)
        /// </summary>
        /// <param name="newValue">local value</param>
        public VariableReference(T newValue)
        {
            LocalValue = newValue;
            referenceType = ReferenceTypes.LOCAL;
            SetLocalValue();
        }

        /// <summary>
        /// Set 'Value' accessor to read and write from local value
        /// </summary>
        private void SetLocalValue()
        {
            GetValue = () => { return LocalValue; };
            SetValue = (T value) => { LocalValue = value; };
        }


        /// <summary>
        /// Constructor with reference to ScriptableObjectVariable containing a value ( ReferenceType = ReferenceTypes.SCRIPTABLEOBJECTVARIABLE)
        /// </summary>
        /// <param name="newScriptableObjectVariable">ScriptableObjectVariable</param>
        public VariableReference(S newScriptableObjectVariable)
        {
            ScriptableObjectValue = newScriptableObjectVariable;
            referenceType = ReferenceTypes.SCRIPTABLEOBJECTVARIABLE;
            SetScriptableObjectValue();
        }

        /// <summary>
        /// Set 'Value' accessor to read and write from ScriptableObject
        /// </summary>
        private void SetScriptableObjectValue()
        {
            GetValue = () => { return ScriptableObjectValue.Value; };
            SetValue = (T value) => { ScriptableObjectValue.Value = value; };
        }


        /// <summary>
        /// Constructor with reference to MonoBehaviourVariable containing a value ( ReferenceType = ReferenceTypes.MONOBEHAVIOURVARIABLE)
        /// </summary>
        /// <param name="newMonoBehaviourVariable">MonoBehaviourVariable</param>
        public VariableReference(M newMonoBehaviourVariable)
        {
            MonoBehaviourValue = newMonoBehaviourVariable;
            referenceType = ReferenceTypes.MONOBEHAVIOURVARIABLE;
            SetMonoBehaviourValue();
        }

        /// <summary>
        /// Set 'Value' accessor to read and write from MonoBehaviour
        /// </summary>
        private void SetMonoBehaviourValue()
        {
            GetValue = () => { return MonoBehaviourValue.Value; };
            SetValue = (T value) => { MonoBehaviourValue.Value = value; };
        }
        


        private delegate T ValueGetter();
        /// <summary>
        /// Delegate to read from the correct value (instead of using a switch case or if/else at each read)
        /// </summary>
        private ValueGetter GetValue = () => { return default; };

        private delegate void ValueSetter(T value);
        /// <summary>
        /// Delegate to write to the correct value (instead of using a switch case or if/else at each write)
        /// </summary>
        private ValueSetter SetValue = (T value) => { };

        /// <summary>
        /// Value referenced
        /// </summary>
        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        /// <summary>
        /// Return the result of Value.ToString()
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ValueToString() => Value.ToString();


        /// <summary>
        /// Allow to read Value directly without having to write 'reference.Value'
        /// </summary>
        public static implicit operator T(VariableReference<T, S, M> Reference)
        {
            return Reference.Value;
        }

        public static implicit operator VariableReference<T, S, M>(T Value)
        {
            return new VariableReference<T, S, M>(Value);
        }


        /// <summary>
        /// Unused yet
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        /// <summary>
        /// ISerializationCallbackReceiver implementation, we use it to set the correct delegate for read and write access to Value
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            switch(referenceType)
            {
                default:
                case ReferenceTypes.LOCAL: SetLocalValue(); break;
                case ReferenceTypes.SCRIPTABLEOBJECTVARIABLE: SetScriptableObjectValue(); break;
                case ReferenceTypes.MONOBEHAVIOURVARIABLE: SetMonoBehaviourValue(); break;
            }
        }
    }
}
