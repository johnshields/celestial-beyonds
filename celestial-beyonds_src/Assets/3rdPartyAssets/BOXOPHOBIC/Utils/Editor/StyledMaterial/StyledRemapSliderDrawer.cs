﻿// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

public class StyledRemapSliderDrawer : MaterialPropertyDrawer
{
    public float down;
    public float max;
    public float min;
    public string nameMax = "";
    public string nameMin = "";

    private bool showAdvancedOptions;
    public float top;

    public StyledRemapSliderDrawer(string nameMin, string nameMax, float min, float max)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.min = min;
        this.max = max;
        top = 0;
        down = 0;
    }

    public StyledRemapSliderDrawer(string nameMin, string nameMax, float min, float max, float top, float down)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.min = min;
        this.max = max;
        this.top = top;
        this.down = down;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        var internalPropMin = MaterialEditor.GetMaterialProperty(editor.targets, nameMin);
        var internalPropMax = MaterialEditor.GetMaterialProperty(editor.targets, nameMax);

        if (internalPropMin.displayName != null && internalPropMax.displayName != null)
        {
            var internalValueMin = internalPropMin.floatValue;
            var internalValueMax = internalPropMax.floatValue;
            var propVector = prop.vectorValue;


            var stylePopup = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9
            };

            EditorGUI.BeginChangeCheck();

            if (internalValueMin <= internalValueMax)
                propVector.w = 0;
            else
                propVector.w = 1;

            if (propVector.w == 0)
            {
                propVector.x = internalValueMin;
                propVector.y = internalValueMax;
            }
            else
            {
                propVector.x = internalValueMax;
                propVector.y = internalValueMin;
            }

            GUILayout.Space(top);

            EditorGUI.showMixedValue = prop.hasMixedValue;

            GUILayout.BeginHorizontal();
            GUILayout.Space(-1);
            GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth));

            EditorGUILayout.MinMaxSlider(ref propVector.x, ref propVector.y, min, max);

            GUILayout.Space(2);

            propVector.w = EditorGUILayout.Popup((int)propVector.w,
                new[] { "Remap", "Invert", "Show Advanced Settings", "Hide Advanced Settings" }, stylePopup,
                GUILayout.Width(50));

            GUILayout.EndHorizontal();

            if (propVector.w == 2f) showAdvancedOptions = true;

            if (propVector.w == 3f) showAdvancedOptions = false;

            if (showAdvancedOptions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Min", GUILayout.Width(EditorGUIUtility.labelWidth));
                propVector.x = EditorGUILayout.Slider(propVector.x, min, max);
                GUILayout.Space(2);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(-1);
                GUILayout.Label("      Remap Max", GUILayout.Width(EditorGUIUtility.labelWidth));
                propVector.y = EditorGUILayout.Slider(propVector.y, min, max);
                GUILayout.Space(2);
                GUILayout.EndHorizontal();
            }

            if (propVector.w == 0f)
            {
                internalValueMin = propVector.x;
                internalValueMax = propVector.y;
            }
            else if (propVector.w == 1f)
            {
                internalValueMin = propVector.y;
                internalValueMax = propVector.x;
            }

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.vectorValue = propVector;
                internalPropMin.floatValue = internalValueMin;
                internalPropMax.floatValue = internalValueMax;
            }

            GUILayout.Space(down);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return -2;
    }
}