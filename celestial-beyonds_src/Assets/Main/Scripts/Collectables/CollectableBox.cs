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