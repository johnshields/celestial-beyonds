using System.Collections;
using UnityEngine;

public class CollectableBox : MonoBehaviour
{
    public AudioClip soundFX;
    public bool broken;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scraper"))
            StartCoroutine(DestroyBox());
    }

    private IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(.5f);
        PlaySound();
        Destroy(gameObject);
    }

    private void PlaySound()
    {
        if (!broken)
        {
            AudioSource.PlayClipAtPoint(soundFX, transform.position, 0.15f);
            broken = true;
        }
    }
}