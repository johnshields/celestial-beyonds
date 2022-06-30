// Cristian Pop - https://boxophobic.com/

using Boxophobic.StyledGUI;
using Boxophobic.Utils;
using UnityEditor;
using UnityEngine;

public class SkyboxExtendedHub : EditorWindow
{
    private static SkyboxExtendedHub window;
    private string assetFolder = "Assets/BOXOPHOBIC/Atmospheric Height Fog";

    private int assetVersion;

    private Color bannerColor;
    private string bannerText;
    private string bannerVersion;
    private string helpURL;

    private void OnEnable()
    {
        //Safer search, there might be many user folders
        string[] searchFolders;

        searchFolders = AssetDatabase.FindAssets("Skybox Cubemap Extended");

        for (var i = 0; i < searchFolders.Length; i++)
            if (AssetDatabase.GUIDToAssetPath(searchFolders[i]).EndsWith("Skybox Cubemap Extended.pdf"))
            {
                assetFolder = AssetDatabase.GUIDToAssetPath(searchFolders[i]);
                assetFolder = assetFolder.Replace("/Skybox Cubemap Extended.pdf", "");
            }

        assetVersion = SettingsUtils.LoadSettingsData(assetFolder + "/Core/Editor/Version.asset", -99);
        bannerVersion = assetVersion.ToString();
        bannerVersion = bannerVersion.Insert(1, ".");
        bannerVersion = bannerVersion.Insert(3, ".");

        bannerColor = new Color(0.95f, 0.61f, 0.46f);
        bannerText = "Skybox Cubemap Extended " + bannerVersion;
        helpURL =
            "https://docs.google.com/document/d/1ughK58Aveoet6hpdfYxY5rzkOcIkjEoR0VdN2AhngSc/edit#heading=h.gqix7il7wlwd";
    }

    private void OnGUI()
    {
        StyledGUI.DrawWindowBanner(bannerColor, bannerText, helpURL);

        GUILayout.BeginHorizontal();
        GUILayout.Space(20);

        EditorGUILayout.HelpBox(
            "The included shader is compatible by default with Standard and Universal Render Pipelines!",
            MessageType.Info, true);

        GUILayout.Space(13);
        GUILayout.EndHorizontal();
    }

    [MenuItem("Window/BOXOPHOBIC/Skybox Cubemap Extended/Hub", false, 1070)]
    public static void ShowWindow()
    {
        window = GetWindow<SkyboxExtendedHub>(false, "Skybox Cubemap Extended", true);
        window.minSize = new Vector2(300, 200);
    }
}