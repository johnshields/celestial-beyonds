//Cristian Pop - https://boxophobic.com/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkyboxExtendedShaderGUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        //base.OnGUI(materialEditor, props);

        var material0 = materialEditor.target as Material;

        DrawDynamicInspector(material0, materialEditor, props);
    }

    private void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
    {
        var customPropsList = new List<MaterialProperty>();

        for (var i = 0; i < props.Length; i++)
        {
            var prop = props[i];

            if (prop.flags == MaterialProperty.PropFlags.HideInInspector)
                continue;

            customPropsList.Add(prop);
        }

        //Draw Custom GUI
        for (var i = 0; i < customPropsList.Count; i++)
        {
            var prop = customPropsList[i];

            materialEditor.ShaderProperty(prop, prop.displayName);
        }

        GUILayout.Space(10);
    }
}