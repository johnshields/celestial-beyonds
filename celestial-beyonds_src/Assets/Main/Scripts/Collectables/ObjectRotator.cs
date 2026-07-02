using UnityEngine;

/*
 * ObjectRotator
 * Script to rotate objects.
*/
public class ObjectRotator : MonoBehaviour
{
    public float speed = 1;

    // Speed is degrees per frame at 60fps; scaled by deltaTime for framerate independence.
    private void Update()
    {
        transform.Rotate(new Vector3(0f, speed * 60f * Time.deltaTime, 0f));
    }
}