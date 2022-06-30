// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Boxophobic.StyledGUI
{
    public class StyledTextureDrawer : MaterialPropertyDrawer
    {
        public float down;
        public float size;
        public float top;

        public StyledTextureDrawer()
        {
            size = 50;
            top = 0;
            down = 0;
        }

        public StyledTextureDrawer(float size)
        {
            this.size = size;
            top = 0;
            down = 0;
        }

        public StyledTextureDrawer(float size, float top, float down)
        {
            this.size = size;
            this.top = top;
            this.down = down;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            GUILayout.Space(top);

            EditorGUI.BeginChangeCheck();

            EditorGUI.showMixedValue = prop.hasMixedValue;

            Texture tex = null;

            if (prop.textureDimension == TextureDimension.Tex2D)
                tex = (Texture2D)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Texture2D),
                    false, GUILayout.Height(50));

            if (prop.textureDimension == TextureDimension.Cube)
                tex = (Cubemap)EditorGUILayout.ObjectField(prop.displayName, prop.textureValue, typeof(Cubemap), false,
                    GUILayout.Height(50));

            EditorGUI.showMixedValue = false;

            if (EditorGUI.EndChangeCheck()) prop.textureValue = tex;

            GUILayout.Space(down);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return -2;
        }
    }
}