// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledMaskDrawer : MaterialPropertyDrawer
    {
        public float down;
        public string options = "";

        public float top;

        public StyledMaskDrawer(string options)
        {
            this.options = options;

            top = 0;
            down = 0;
        }

        public StyledMaskDrawer(string options, float top, float down)
        {
            this.options = options;

            this.top = top;
            this.down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            var enums = options.Split(char.Parse("_"));

            GUILayout.Space(top);

            var mask = (int)prop.floatValue;

            mask = EditorGUILayout.MaskField(prop.displayName, mask, enums);

            if (mask < 0) mask = -1;

            // Debug Value
            //EditorGUILayout.LabelField(mask.ToString());

            prop.floatValue = mask;

            GUI.enabled = true;

            GUILayout.Space(down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}