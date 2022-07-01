using System.Collections;
using System.Collections.Generic;
using Main.Scripts.Combat;
using UnityEngine;

public class HoneyJars : MonoBehaviour
{
    public AudioClip pickupSound;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player && _player.GetComponent<CaptainHealth>().currentHealth != 30)
        {
            _player.GetComponent<CaptainHealth>().PlayerGainHealth(5);
            var position = transform.position;
            AudioSource.PlayClipAtPoint(pickupSound, position, 0.1f);
            Destroy(gameObject);
        }
    }
}