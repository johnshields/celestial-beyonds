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
        
        var collisionModule = GetComponent<ParticleSystem>().collision;
        if (other.gameObject.CompareTag("CollectableBox") && !other.gameObject.CompareTag("Enemy"))
        {
            collisionModule.maxCollisionShapes = 1000;
            other.gameObject.GetComponent<CollectableBox>().IfCannon();
        }
        else
        {
            collisionModule.maxCollisionShapes = 1;
        }
        
        if (other.gameObject.CompareTag("Enemy") && _player.GetComponent<CaptainAnimAndSound>().endgame)
        {
            other.gameObject.GetComponent<AristauesProfiler>().TakeDamage(other.gameObject);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}