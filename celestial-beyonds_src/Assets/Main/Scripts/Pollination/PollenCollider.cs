using UnityEngine;

public class PollenCollider : MonoBehaviour
{
    private GameObject _plant, _pollinationLevel;

    private void Start()
    {
        _plant = GameObject.FindGameObjectWithTag("Sunflower");
        _pollinationLevel = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Pollen hit a plant");
        _plant.GetComponent<Plants>().GrowPlant();
        _pollinationLevel.GetComponent<PollinationLevel>().pollinationPercent += 1;
    }
}