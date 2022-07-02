using System;
using UnityEngine;
using CobayeStudio.VariableReference;


namespace CobayeStudio.VariableReference.Example
{
    public class VariableReferenceExample : MonoBehaviour
    {
        public boolReference myBool;
        [Space]
        public intReference myInt;
        [Space]
        public floatReference myFloat;
        [Space]
        public doubleReference myDouble;
        [Space]
        public stringReference myString;
        [Space]
        public Vector2Reference myVector2;
        [Space]
        public Vector3Reference myvector3;
        [Space]
        public QuaternionReference myQuaternion;
        [Space]
        public ExampleSerializableClassReference myExampleClass;
    }

    [Serializable]
    public class ExampleSerializableClass
    {
        public int someInt;
        public Vector3 someVector;
    }
}
