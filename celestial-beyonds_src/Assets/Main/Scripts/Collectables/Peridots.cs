using UnityEngine;

public class Peridots : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject miniMenu;
    public int peridotValue = 1;
    private Component _peridotCounter;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _peridotCounter = _player.GetComponent<PeridotCounter>();
    }

    // Allows only the Player to collect the _peridotCounter.
    // Plus adds collected peridots to the _peridotCounter.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _player) return;
        var position = transform.position;
        AudioSource.PlayClipAtPoint(pickupSound, position, 0.1f);
        // Destroy peridot and add to the _peridotCounter
        Destroy(gameObject);
        _peridotCounter.GetComponent<PeridotCounter>().peridots += peridotValue;
        miniMenu.GetComponent<MiniMenu>().peridotsNum += 1;
    }
}