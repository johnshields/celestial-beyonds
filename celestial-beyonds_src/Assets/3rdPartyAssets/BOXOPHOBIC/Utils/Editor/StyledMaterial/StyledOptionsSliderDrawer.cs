﻿// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

public class StyledOptionsSliderDrawer : MaterialPropertyDrawer
{
    public float down;
    public float max;
    public float min;
    public string nameMax = "";
    public string nameMin = "";
    public string nameVal = "";
    public float top;
    public float val;

    public StyledOptionsSliderDrawer(string nameMin, string nameMax, string nameVal, float min, float max, float val)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.nameVal = nameVal;
        this.min = min;
        this.max = max;
        this.val = val;
        top = 0;
        down = 0;
    }

    public StyledOptionsSliderDrawer(string nameMin, string nameMax, string nameVal, float min, float max, float val,
        float top, float down)
    {
        this.nameMin = nameMin;
        this.nameMax = nameMax;
        this.nameVal = nameVal;
        this.min = min;
        this.max = max;
        this.val = val;
        this.top = top;
        this.down = down;
    }

    public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        var internalPropMin = MaterialEditor.GetMaterialProperty(editor.targets, nameMin);
        var internalPropMax = MaterialEditor.GetMaterialProperty(editor.targets, nameMax);
        var internalPropVal = MaterialEditor.GetMaterialProperty(editor.targets, nameVal);

        if (internalPropMin.displayName != null && internalPropMax.displayName != null &&
            internalPropVal.displayName != null)
        {
            var stylePopup = new GUIStyle(EditorStyles.popup)
            {
                fontSize = 9
            };

            var internalValueMin = internalPropMin.floatValue;
            var internalValueMax = internalPropMax.floatValue;
            var internalValueVal = internalPropVal.floatValue;
            var propVector = prop.vectorValue;

            EditorGUI.BeginChangeCheck();

            if (propVector.w == 2)
            {
                propVector.x = min;
                propVector.y = max;
                propVector.z = internalValueVal;
            }
            else
            {
                if (internalValueMin < internalValueMax)
                    propVector.w = 0;
                else if (internalValueMin < internalValueMax) propVector.w = 1;

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

                propVector.z = val;
            }

            GUILayout.Space(top);

            EditorGUI.showMixedValue = prop.hasMixedValue;

            GUILayout.BeginHorizontal();
            GUILayout.Space(-1);
            GUILayout.Label(label, GUILayout.Width(EditorGUIUtility.labelWidth));

            if (propVector.w == 2)
                propVector.z = GUILayout.HorizontalSlider(propVector.z, min, max);
            else
                EditorGUILayout.MinMaxSlider(ref propVector.x, ref propVector.y, min, max);

            GUILayout.Space(2);

            propVector.w = EditorGUILayout.Popup((int)propVector.w, new[] { "Remap", "Invert", "Simple" }, stylePopup,
                GUILayout.Width(50));

            GUILayout.EndHorizontal();

            if (propVector.w == 0f)
            {
                internalValueMin = propVector.x;
                internalValueMax = propVector.y;
                internalValueVal = val;
            }
            else if (propVector.w == 1f)
            {
                internalValueMin = propVector.y;
                internalValueMax = propVector.x;
                internalValueVal = val;
            }
            else if (propVector.w == 2f)
            {
                internalValueMin = min;
                internalValueMax = max;
                internalValueVal = propVector.z;
            }

            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                prop.vectorValue = propVector;
                internalPropMin.floatValue = internalValueMin;
                internalPropMax.floatValue = internalValueMax;
                internalPropVal.floatValue = internalValueVal;
            }

            GUILayout.Space(down);
        }
    }

    public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
    {
        return -2;
    }
}