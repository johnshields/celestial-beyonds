using UnityEngine;

public class CelestialDefier : MonoBehaviour
{
    public GameObject attachments, site;
    public Material celestialBlue;

    public void SummonCelestialDefier()
    {
        attachments.SetActive(true);
        site.SetActive(false);
        var rend = gameObject.GetComponent<MeshRenderer>();
        var materials = rend.sharedMaterials; // read current array of materials
        materials[2] = celestialBlue;
        rend.sharedMaterials = materials; 
    }
}
