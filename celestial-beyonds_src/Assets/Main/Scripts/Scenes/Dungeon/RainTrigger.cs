using UnityEngine;

public class RainTrigger : MonoBehaviour
{
    public GameObject rain;
    public bool rainOnOrOff;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rain.SetActive(rainOnOrOff);
        }
        
    }
}
