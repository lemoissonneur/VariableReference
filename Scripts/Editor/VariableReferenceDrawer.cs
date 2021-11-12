using UnityEditor;
using UnityEngine;


namespace CobayeStudio.VariableReference
{
    /// <summary>
    /// VariableReference inspector drawer class.
    /// </summary>
    [CustomPropertyDrawer(typeof(VariableReference), true)]
    public class VariableReferenceDrawer : PropertyDrawer
    {
        #region properties & Rect 

        /// <summary> Options to display in the popup to select constant or variable. </summary>
        private readonly string[] _PopupOption = { "Local", "ScriptableObject", "MonoBehaviour" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle _PopupStyle;

        private Rect LabelRect(Rect propertyPosition) => new Rect(propertyPosition)
        {
            width = EditorGUIUtility.labelWidth,
            height = EditorGUIUtility.singleLineHeight,
        };

        private Rect ButtonRect(Rect propertyPosition) => new Rect(propertyPosition)
        {
            xMin = propertyPosition.xMin + EditorGUIUtility.labelWidth,
            yMin = propertyPosition.yMin + _PopupStyle.margin.top,
            width = _PopupStyle.fixedWidth + _PopupStyle.margin.right,
            height = EditorGUIUtility.singleLineHeight,
        };

        private Rect FieldRect(Rect propertyPosition) => new Rect(propertyPosition)
        {
            xMin = ButtonRect(propertyPosition).xMax,
            height = EditorGUIUtility.singleLineHeight,
        };

        private Rect ValueRect(Rect propertyPosition) => new Rect(propertyPosition)
        {
            yMin = propertyPosition.yMin + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
            xMin = propertyPosition.xMin + 10,
        };

        #endregion



        #region PropertyDrawer Override

        public override bool CanCacheInspectorGUI(SerializedProperty property) => true;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool isBaseClass = property.FindPropertyRelative("LocalValue") == null;

            SerializedProperty referenceTypeProperty = property.FindPropertyRelative("referenceType");
            VariableReference.ReferenceTypes referenceType = (VariableReference.ReferenceTypes)referenceTypeProperty.enumValueIndex;

            SerializedProperty objectReferenceProperty = GetObjectReferenceProperty(property, referenceType, isBaseClass);

            SerializedProperty valueProperty = GetValueProperty(property, objectReferenceProperty, referenceType, isBaseClass);

            float ValueHeight = valueProperty != null && property.isExpanded ?
                EditorGUI.GetPropertyHeight(valueProperty, label) + EditorGUIUtility.standardVerticalSpacing :
                0;

            return EditorGUIUtility.singleLineHeight + ValueHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // set up popup style
            if (_PopupStyle == null)
            {
                _PopupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
                {
                    imagePosition = ImagePosition.ImageOnly
                };
            }

            // are we drawing the base class or a type specified class ?
            bool isBaseClass = property.FindPropertyRelative("LocalValue") == null;

            SerializedProperty referenceTypeProperty = property.FindPropertyRelative("referenceType");
            VariableReference.ReferenceTypes referenceType = (VariableReference.ReferenceTypes)referenceTypeProperty.enumValueIndex;

            SerializedProperty objectReferenceProperty = GetObjectReferenceProperty(property, referenceType, isBaseClass);

            SerializedProperty valueProperty = GetValueProperty(property, objectReferenceProperty, referenceType, isBaseClass);

            // draw label
            label = EditorGUI.BeginProperty(position, label, property);

            property.isExpanded = EditorGUI.Foldout(LabelRect(position), property.isExpanded, label);

            EditorGUI.BeginChangeCheck();

            // draw reference type popup
            referenceTypeProperty.enumValueIndex = EditorGUI.Popup(ButtonRect(position), referenceTypeProperty.enumValueIndex, _PopupOption, _PopupStyle);

            if(referenceType != VariableReference.ReferenceTypes.LOCAL)
                EditorGUI.PropertyField(FieldRect(position), objectReferenceProperty, GUIContent.none);

            if (valueProperty != null && property.isExpanded)
                EditorGUI.PropertyField(ValueRect(position), valueProperty, new GUIContent("Value"), true);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                valueProperty?.serializedObject.ApplyModifiedProperties();
            }

            EditorGUI.EndProperty();
        }

        #endregion



        private SerializedProperty GetObjectReferenceProperty(SerializedProperty property, VariableReference.ReferenceTypes referenceType, bool isBaseClass)
        {
            SerializedProperty ObjectReferenceProperty = null;

            switch (referenceType)
            {
                case VariableReference.ReferenceTypes.SCRIPTABLEOBJECTVARIABLE:
                    ObjectReferenceProperty = property.FindPropertyRelative(isBaseClass ? "GenericScriptableObjectValue" : "ScriptableObjectValue");
                    break;
                case VariableReference.ReferenceTypes.MONOBEHAVIOURVARIABLE:
                    ObjectReferenceProperty = property.FindPropertyRelative(isBaseClass ? "GenericMonoBehaviourValue" : "MonoBehaviourValue");
                    break;
            }

            return ObjectReferenceProperty;
        }

        private SerializedProperty GetValueProperty(SerializedProperty property, SerializedProperty objectReferenceProperty, VariableReference.ReferenceTypes referenceType, bool isBaseClass)
        {
            SerializedProperty ValueProperty = null;

            if (referenceType == VariableReference.ReferenceTypes.LOCAL)
            {
                ValueProperty = isBaseClass ? null : property.FindPropertyRelative("LocalValue");
            }
            else if (objectReferenceProperty != null && objectReferenceProperty.objectReferenceValue != null)
            {
                ValueProperty = new SerializedObject(objectReferenceProperty.objectReferenceValue).FindProperty("value");
            }

            return ValueProperty;
        }
    }
}