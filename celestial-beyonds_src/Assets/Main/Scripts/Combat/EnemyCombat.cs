using UnityEngine;

/*
 * EnemyCombat
 * Attached to Enemy - triggered when they hit player.
 */
namespace Main.Scripts.Combat
{
    public class EnemyCombat : MonoBehaviour
    {
        private GameObject _player;
        public int damageAmount;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                _player.GetComponent<CaptainHealth>().PlayerTakeDamage(damageAmount);
        }
    }
}