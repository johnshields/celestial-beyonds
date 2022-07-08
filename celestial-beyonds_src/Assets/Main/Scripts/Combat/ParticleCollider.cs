using System;
using Main.Scripts.Enemies;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    private bool _shot;

    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit: " + other.gameObject.name);
        if (!_shot && other.gameObject.CompareTag("Enemy"))
        {
            _shot = true;
            other.gameObject.GetComponent<EnemyProfiler>().TakeDamage(other.gameObject);
            Invoke(nameof(RechargeShot), 2);
        }
        else if (!_shot && other.gameObject.CompareTag("CollectableBox"))
        {
            _shot = true;
            other.gameObject.GetComponent<CollectableBox>().IfCannon();
            Invoke(nameof(RechargeShot), 2);
        }
    }

    private void RechargeShot()
    {
        _shot = false;
    }
}