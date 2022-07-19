using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class CollectableBox : MonoBehaviour
{
    public AudioClip soundFX;
    private GameObject _player;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Scraper") && _player.GetComponent<CaptainAnimAndSound>().meleeActive)
            StartCoroutine(DestroyBox());
    }

    public void IfCannon()
    {
        AudioSource.PlayClipAtPoint(soundFX, transform.position, 0.15f);
        Destroy(gameObject);
    }
    
    private IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(.5f);
        AudioSource.PlayClipAtPoint(soundFX, transform.position, 0.15f);
        Destroy(gameObject);
    }
}