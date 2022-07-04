using UnityEngine;

public class PollenCollider : MonoBehaviour
{
    private GameObject _plant;
    private bool _plantGrown;

    private void Start()
    {
        _plant = GameObject.FindGameObjectWithTag("Sunflower");
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Pollen hit a plant");
        if (!_plantGrown)
        {
            _plantGrown = true;
            _plant.GetComponent<Plants>().GrowPlant();
            Invoke(nameof(ResetPlant), 4);
        }
    }

    private void ResetPlant()
    {
        _plantGrown = false;
    }
}