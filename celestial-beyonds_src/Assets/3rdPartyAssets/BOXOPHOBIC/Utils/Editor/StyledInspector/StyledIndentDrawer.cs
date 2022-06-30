// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledIndent))]
    public class StyledIndentAttributeDrawer : PropertyDrawer
    {
        private StyledIndent a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledIndent)attribute;

            EditorGUI.indentLevel = a.indent;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}