// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledButtonDrawer : MaterialPropertyDrawer
    {
        public float down;
        public string target = "";
        public string text;
        public float top;
        public float value = 1;

        public StyledButtonDrawer(string text)
        {
            this.text = text;
            value = 1;
            top = 0;
            down = 0;
        }

        public StyledButtonDrawer(string text, float value, float top, float down)
        {
            this.text = text;
            this.value = value;
            this.top = top;
            this.down = down;
        }

        public StyledButtonDrawer(string text, string target, float value, float top, float down)
        {
            this.text = text;
            this.target = target;
            this.value = value;
            this.top = top;
            this.down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            var material = materialEditor.target as Material;

            GUILayout.Space(top);

            if (GUILayout.Button(text))
            {
                if (target == "")
                {
                    prop.floatValue = value;
                }
                else
                {
                    if (material.HasProperty(target)) material.SetFloat(target, value);
                }
            }

            GUILayout.Space(down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}