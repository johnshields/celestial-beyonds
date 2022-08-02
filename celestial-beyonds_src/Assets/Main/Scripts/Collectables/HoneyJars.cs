using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoneyJars : MonoBehaviour
{
    public AudioClip pickupSound;
    public int healthAmount = 5;
    private GameObject _player;
    private GameObject healthBar;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
    }

    private void OnTriggerEnter(Collider other)
    {
        // only heal player if they do not have maxHealth.
        if (other.gameObject == _player && _player.GetComponent<CaptainHealth>().currentHealth !=
            _player.GetComponent<CaptainHealth>().maxHealth)
        {
            _player.GetComponent<CaptainHealth>().PlayerGainHealth(healthAmount);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 0.2f);
            Destroy(gameObject);
        }
        // if they do flash the healthBar UI
        else if (other.gameObject == _player && _player.GetComponent<CaptainHealth>().currentHealth ==
                 _player.GetComponent<CaptainHealth>().maxHealth)
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