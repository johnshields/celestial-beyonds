using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenCollider : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Pollen hit a plant");
    }
}
