using Main.Scripts.Enemies;
using UnityEngine;

/*
 * CaptainCombat
 * Attached to Player's Scraper - triggered when it hits enemies.
 */
namespace Main.Scripts.Combat
{
    public class CaptainCombat : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
                other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(1, other.gameObject);
        }
    }
}