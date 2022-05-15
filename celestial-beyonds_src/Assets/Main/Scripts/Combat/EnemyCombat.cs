using UnityEngine;

/*
 * EnemyCombat
 * Attached to Enemy - triggered when they hit player.
 */
namespace Main.Scripts.Combat
{
    public class EnemyCombat : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && CombatManager.playerHealth >= 0)
                CombatManager.playerHealth--;
            
            print("Cap's Health: " + CombatManager.playerHealth);
        }
    }
}