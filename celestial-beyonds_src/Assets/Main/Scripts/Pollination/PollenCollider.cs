using System.Text.RegularExpressions;
using UnityEngine;

public class PollenCollider : MonoBehaviour
{
    private GameObject _plant;

    private void Start()
    {
        _plant = GameObject.FindGameObjectWithTag("Plants");
    }

    private void OnParticleCollision(GameObject other)
    {
        var numberOnly = Regex.Replace(other.gameObject.name, "[^0-9]", "");
        var plantNum = int.Parse(numberOnly);
        print("Pollen hit plant: " + plantNum);
        if (other.CompareTag("Plant"))
        {
            _plant.GetComponent<Plants>().Blossom(plantNum);
        }
    }
}