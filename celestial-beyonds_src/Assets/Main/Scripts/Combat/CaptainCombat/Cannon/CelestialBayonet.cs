using Main.Scripts.Enemies;
using UnityEngine;

public class CelestialBayonet : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
