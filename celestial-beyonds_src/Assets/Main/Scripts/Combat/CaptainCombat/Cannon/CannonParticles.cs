using Main.Scripts.Captain;
using Main.Scripts.Enemies;
using UnityEngine;

public class CannonParticles : MonoBehaviour
{

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy") && !_player.GetComponent<CaptainAnimAndSound>().endgame)
        {
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }

        if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().endgame)
        {
            other.gameObject.GetComponent<AristauesProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}