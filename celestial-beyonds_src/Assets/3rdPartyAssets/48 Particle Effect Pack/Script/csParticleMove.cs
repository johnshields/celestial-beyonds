using UnityEngine;

public class csParticleMove : MonoBehaviour
{
    public float speed = 0.1f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed);
    }
}