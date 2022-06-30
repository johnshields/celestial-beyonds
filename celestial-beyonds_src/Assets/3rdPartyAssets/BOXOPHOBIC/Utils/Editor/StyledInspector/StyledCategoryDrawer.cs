// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledCategory))]
    public class StyledCategoryAttributeDrawer : PropertyDrawer
    {
        private StyledCategory a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledCategory)attribute;

            GUI.enabled = true;
            EditorGUI.indentLevel = 0;

            GUILayout.Space(a.top);
            StyledGUI.DrawInspectorCategory(a.category);
            GUILayout.Space(a.down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}