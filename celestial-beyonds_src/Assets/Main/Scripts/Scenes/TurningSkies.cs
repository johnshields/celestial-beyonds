using UnityEngine;

public class TurningSkies : MonoBehaviour
{
    public Color redSkies = new Color32(75, 18, 43, 225);
    public Color blueSkies = new Color32(20, 61, 94, 225);
    public Color fog = new Color32(60, 67, 107, 225);
    public float duration = 1.0F;

    public GameObject[] rain;

    private void Update()
    {
        rain[0].SetActive(false);
        rain[1].SetActive(false);
        RenderSettings.fogColor = fog;
        RenderSettings.fogDensity = 0.01f;
        var lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor($"_TintColor", Color.Lerp(redSkies, blueSkies, lerp));
    }
}