using UnityEngine;

public class MoonbeamProfiler : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform);
    }
}
