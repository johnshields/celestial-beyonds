// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledEnum))]
    public class StyledEnumAttributeDrawer : PropertyDrawer
    {
        private StyledEnum a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledEnum)attribute;

            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            var enums = a.options.Split(char.Parse("_"));

            GUILayout.Space(a.top);

            var index = property.intValue;

            index = EditorGUILayout.Popup(property.displayName, index, enums);

            // Debug Value
            //EditorGUILayout.LabelField(mask.ToString());

            property.intValue = index;

            GUI.enabled = true;

            GUILayout.Space(a.down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}