using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CobayeStudio.VariableReference;


namespace CobayeStudio.VariableReference.PlayTests
{
    public class VariableReferencePlayTests
    {
        [UnityTest]
        public IEnumerator TestAllPrimitives()
        {
            VariableReferenceTestHelper<bool>(true, false);
            VariableReferenceTestHelper<int>(-10, 20);
            VariableReferenceTestHelper<float>(-7.8f, 14.75f);
            VariableReferenceTestHelper<double>(-102.786, 1378.4567);
            VariableReferenceTestHelper<string>("string A", "string B");
            VariableReferenceTestHelper<Vector2>(Vector2.up, Vector2.down);
            VariableReferenceTestHelper<Vector3>(Vector3.left, Vector3.right);
            VariableReferenceTestHelper<Quaternion>(Random.rotation, Random.rotation);

            return null;
        }

        public void VariableReferenceTestHelper<T>(T a, T b)
        {
            // if the two values are equals, test result are compromised
            Assert.AreNotEqual(a, b);

            CanReadAndWriteLocal<T>(a, b);
            CanReadAndWriteScriptableObject<T>(a, b);
            CanReadAndWriteMonoBehaviour<T>(a, b);
        }

        public void CanReadAndWriteLocal<T>(T a, T b)
        {
            // create a new local reference with value 'a'
            VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>> reference = new VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>>(a);

            // check we can can read the value
            Assert.AreEqual(reference.Value, a);

            // write another value
            reference.Value = b;

            // check writen value is correct
            Assert.AreEqual(reference.Value, b);
        }

        public IEnumerator CanReadAndWriteScriptableObject<T>(T a, T b)
        {
            // create a new SO with value 'a'
            ScriptableObjectVariable<T> so = ScriptableObject.CreateInstance<ScriptableObjectVariable<T>>();
            yield return null;  // skip a frame otherwhise so = nullRef
            so.Value = a;

            // create a new SO reference
            VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>> reference;
            reference = new VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>>(so);

            // check the value we read in the reference is the value set in the SO
            Assert.AreEqual(reference.Value, a);

            // write another value
            reference.Value = b;

            // check the writen value is the one we read in the SO
            Assert.AreEqual(so.Value, b);
        }

        public IEnumerator CanReadAndWriteMonoBehaviour<T>(T a, T b)
        {
            // create a new mono with value 'a'
            GameObject g = new GameObject();
            yield return null;  // skip a frame otherwhise go = nullRef
            MonoBehaviourVariable<T> mono = g.AddComponent<MonoBehaviourVariable<T>>();
            yield return null;  // skip a frame otherwhise mono = nullRef
            mono.Value = a;

            // create a new Mono reference
            VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>> reference;
            reference = new VariableReference<T, ScriptableObjectVariable<T>, MonoBehaviourVariable<T>>(mono);

            // check the value we read in the reference is the value set in the Mono
            Assert.AreEqual(reference.Value, a);

            // write another value
            reference.Value = b;

            // check the writen value is the one we read in the Mono
            Assert.AreEqual(mono.Value, b);
        }
    }
}
