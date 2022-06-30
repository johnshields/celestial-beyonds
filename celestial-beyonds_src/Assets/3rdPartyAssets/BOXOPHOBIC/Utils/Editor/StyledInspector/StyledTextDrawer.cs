// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledText))]
    public class StyledTextAttributeDrawer : PropertyDrawer
    {
        private StyledText a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledText)attribute;

            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                wordWrap = true
            };

            styleLabel.alignment = a.alignment;

            GUILayout.Space(a.top);

            if (a.disabled) GUI.enabled = false;

            GUILayout.Label(property.stringValue, styleLabel);

            GUI.enabled = true;

            GUILayout.Space(a.down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}