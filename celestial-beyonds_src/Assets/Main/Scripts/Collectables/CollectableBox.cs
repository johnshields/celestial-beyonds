using System;
using UnityEngine;

public class CollectableBox : MonoBehaviour
{
    public AudioClip soundFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scraper"))
        {
            AudioSource.PlayClipAtPoint(soundFX, transform.position, 0.25f);
            Destroy(gameObject);   
        }
    }

    public void IfCannon()
    {
        AudioSource.PlayClipAtPoint(soundFX, transform.position, 0.5f);
        Destroy(gameObject);   
    }
}
