// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledButton))]
    public class StyledButtonAttributeDrawer : PropertyDrawer
    {
        private StyledButton a;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            a = (StyledButton)attribute;

            GUILayout.Space(a.Top);

            if (GUILayout.Button(a.Text)) property.boolValue = true;

            GUILayout.Space(a.Down);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}