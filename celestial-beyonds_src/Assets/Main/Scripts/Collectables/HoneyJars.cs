using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoneyJars : MonoBehaviour
{
    public AudioClip pickupSound;
    private GameObject _player;
    private GameObject healthBar;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
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
        else if (other.gameObject ==_player && _player.GetComponent<CaptainHealth>().currentHealth == 30)
        {
            healthBar.GetComponent<Image>().color = new Color32(60, 20, 150, 225);
            StartCoroutine(ResetHealthColor());
        }
    }

    private IEnumerator ResetHealthColor()
    {
        yield return new WaitForSeconds(1);
        healthBar.GetComponent<Image>().color = new Color32(20, 150, 20, 225);
    }
}