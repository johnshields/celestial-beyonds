﻿// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledToggleDrawer : MaterialPropertyDrawer
    {
        public float width;

        public StyledToggleDrawer()
        {
        }

        public StyledToggleDrawer(float width)
        {
            this.width = width;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            //Material material = materialEditor.target as Material;

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = prop.hasMixedValue;

            if (width == 0)
            {
                var toggle = false;

                if (prop.floatValue > 0.5f) toggle = true;

                toggle = EditorGUILayout.Toggle(label, toggle);

                EditorGUI.showMixedValue = false;

                if (EditorGUI.EndChangeCheck())
                {
                    if (toggle)
                        prop.floatValue = 1;
                    else
                        prop.floatValue = 0;
                }
            }
            else
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(label);

                var toggle = false;

                if (prop.floatValue > 0.5f) toggle = true;

                toggle = GUILayout.Toggle(toggle, "", GUILayout.Width(width));

                EditorGUI.showMixedValue = false;

                if (EditorGUI.EndChangeCheck())
                {
                    if (toggle)
                        prop.floatValue = 1;
                    else
                        prop.floatValue = 0;
                }

                GUILayout.EndHorizontal();
            }
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}