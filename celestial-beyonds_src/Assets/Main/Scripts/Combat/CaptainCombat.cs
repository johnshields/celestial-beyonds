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
            if (other.gameObject.CompareTag("Enemy") && CombatManager.enemyHealth >= 0)
                CombatManager.enemyHealth--;

            print("Enemy Health: " + CombatManager.enemyHealth);
        }
    }
}