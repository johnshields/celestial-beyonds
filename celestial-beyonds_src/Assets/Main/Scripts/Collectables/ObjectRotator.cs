using UnityEngine;

/*
 * ObjectRotator
 * Script to rotate objects.
*/
public class ObjectRotator : MonoBehaviour
{
    public float speed = 1;
    private void Update()
    {
        transform.Rotate(new Vector3(0f, speed, 0f));
    }
}