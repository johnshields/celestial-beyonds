using Main.Scripts.Enemies;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(1, other.gameObject);
    }
}