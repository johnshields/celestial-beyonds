using UnityEngine;

public class Kraken : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.GetComponent<CaptainHealth>().PlayerTakeDamage(300);
        }
    }
}
