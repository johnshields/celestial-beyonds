using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialRedSkies : MonoBehaviour
{
    public Color redSkies = new Color32(75, 18, 43, 225);
    
    private void Awake()
    {
        RenderSettings.skybox.SetColor($"_TintColor", redSkies);
    }
}
