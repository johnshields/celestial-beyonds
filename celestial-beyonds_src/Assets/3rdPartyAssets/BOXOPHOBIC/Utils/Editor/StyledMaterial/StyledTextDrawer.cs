﻿// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
    public class StyledTextDrawer : MaterialPropertyDrawer
    {
        public string alignment = "Center";
        public string disabled = "";
        public float down;
        public string font = "Normal";
        public float size = 11;
        public string text = "";
        public float top;

        public StyledTextDrawer(string text)
        {
            this.text = text;
        }

        public StyledTextDrawer(string text, string alignment, string font, string disabled, float size)
        {
            this.text = text;
            this.alignment = alignment;
            this.font = font;
            this.disabled = disabled;
            this.size = size;
        }

        public StyledTextDrawer(string text, string alignment, string font, string disabled, float size, float top,
            float down)
        {
            this.text = text;
            this.alignment = alignment;
            this.font = font;
            this.disabled = disabled;
            this.size = size;
            this.top = top;
            this.down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            //Material material = materialEditor.target as Material;

            var styleLabel = new GUIStyle(EditorStyles.label)
            {
                richText = true,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true
            };

            GUILayout.Space(top);

            if (alignment == "Center")
                styleLabel.alignment = TextAnchor.MiddleCenter;
            else if (alignment == "Left")
                styleLabel.alignment = TextAnchor.MiddleLeft;
            else if (alignment == "Right") styleLabel.alignment = TextAnchor.MiddleRight;

            if (font == "Normal")
                styleLabel.fontStyle = FontStyle.Normal;
            else if (font == "Bold")
                styleLabel.fontStyle = FontStyle.Bold;
            else if (font == "Italic")
                styleLabel.fontStyle = FontStyle.Italic;
            else if (font == "BoldAndItalic") styleLabel.fontStyle = FontStyle.BoldAndItalic;

            styleLabel.fontSize = (int)size;

            if (disabled == "Disabled") GUI.enabled = false;

            GUILayout.Label(text, styleLabel);

            GUI.enabled = true;

            GUILayout.Space(down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}