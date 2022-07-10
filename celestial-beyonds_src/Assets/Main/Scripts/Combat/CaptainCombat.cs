using Main.Scripts.Captain;
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
        private GameObject _player;
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().meleeActive)
                other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
        }
    }
}