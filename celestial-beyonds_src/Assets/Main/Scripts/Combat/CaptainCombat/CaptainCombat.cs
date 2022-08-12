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
            // Only allow Scraper to hit off Enemies
            if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().meleeActive && 
                !_player.GetComponent<CaptainAnimAndSound>().endgame)
            {
                // Enemies take damage and play a 'blood' particle effect
                other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
                other.gameObject.GetComponent<ParticleSystem>().Play();
            }
            
            if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().meleeActive && 
                _player.GetComponent<CaptainAnimAndSound>().endgame)
            {
                other.gameObject.GetComponent<AristauesProfiler>().TakeDamage(other.gameObject);
                other.gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}