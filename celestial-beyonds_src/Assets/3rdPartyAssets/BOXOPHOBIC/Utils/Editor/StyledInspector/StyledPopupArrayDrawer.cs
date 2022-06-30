// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledPopupArray))]
    public class StyledPopupArrayAttributeDrawer : PropertyDrawer
    {
        private StyledPopupArray a;
        private int index;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledPopupArray)attribute;

            var arrProp = property.serializedObject.FindProperty(a.array);

            var arr = new string[arrProp.arraySize];

            for (var i = 0; i < arrProp.arraySize; i++) arr[i] = arrProp.GetArrayElementAtIndex(i).stringValue;

            index = EditorGUILayout.Popup(property.displayName, index, arr);
            property.intValue = index;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}