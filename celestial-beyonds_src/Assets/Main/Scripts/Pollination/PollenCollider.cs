using System.Text.RegularExpressions;
using UnityEngine;

public class PollenCollider : MonoBehaviour
{
    private GameObject _plant;
    private bool _plantGrown;

    private void Start()
    {
        _plant = GameObject.FindGameObjectWithTag("Plants");
    }

    private void OnParticleCollision(GameObject other)
    {
        var numberOnly = Regex.Replace(other.gameObject.name, "[^0-9]", "");
        var plantNum = int.Parse(numberOnly);
        print("Pollen hit plant: " + plantNum);
        if (_plant.gameObject.GetComponent<Plants>().plantsOG[plantNum])
        {
            _plantGrown = true;
            _plant.GetComponent<Plants>().Blossom(plantNum);
        }
    }

    // private void ResetPlant()
    // {
    //     _plantGrown = false;
    //     print("Plant reset.");
    // }
}