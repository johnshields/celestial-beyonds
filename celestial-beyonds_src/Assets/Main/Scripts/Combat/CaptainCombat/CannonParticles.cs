using Main.Scripts.Enemies;
using UnityEngine;

public class CannonParticles : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else if (other.gameObject.CompareTag("CollectableBox"))
        {
            other.gameObject.GetComponent<CollectableBox>().IfCannon();
        }
    }
}