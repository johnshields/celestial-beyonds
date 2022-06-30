using UnityEngine;

public class ParticleCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Cannon hit an object");
    }
}