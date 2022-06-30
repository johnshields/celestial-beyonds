// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    [CustomPropertyDrawer(typeof(StyledPopupLayers))]
    public class StyledPopupLayersAttributeDrawer : PropertyDrawer
    {
        private int index;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            index = property.intValue;

            var allLayers = new string[32];

            for (var i = 0; i < 32; i++)
                if (LayerMask.LayerToName(i).Length < 1)
                    allLayers[i] = "Missing";
                else
                    allLayers[i] = LayerMask.LayerToName(i);

            index = EditorGUILayout.Popup(property.displayName, index, allLayers);
            property.intValue = index;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2;
        }
    }
}