using UnityEngine;
using UnityEngine.AI;

public class NavMeshInit : MonoBehaviour
{
    private void Start()
    {
        if (NavMesh.SamplePosition(gameObject.transform.position, out var closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");
    }
}