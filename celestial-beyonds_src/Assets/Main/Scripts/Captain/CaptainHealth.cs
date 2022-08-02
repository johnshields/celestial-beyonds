using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.UI;

public class CaptainHealth : MonoBehaviour
{
    public bool capDead, gameOver;
    public int maxHealth = 100, currentHealth;
    public GameObject pHealthBar, GameOverUI;
    public Slider pHealthBarSlider;
    public AudioClip terminatedSFX;
    private GameObject _player;
    private bool _played;
    private AudioSource _audio;

    private void Start()
    {
        currentHealth = maxHealth;
        pHealthBarSlider = pHealthBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        pHealthBarSlider.value = currentHealth;

        if (currentHealth >= maxHealth && !GetComponent<CaptainAnimAndSound>().aUpgrade && !Bools.aUpgraded)
            currentHealth = 100;
        else if (currentHealth >= maxHealth && GetComponent<CaptainAnimAndSound>().aUpgrade && Bools.aUpgraded)
            currentHealth = 200;
    }

    public void PlayerTakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            capDead = true;
            print("Player Terminated!");
            _player.GetComponent<CaptainAnimAndSound>().CapDeath();
            StartCoroutine(GameOverScreen());
        }
    }

    public void PlayerGainHealth(int amount)
    {
        if (currentHealth != maxHealth)
            currentHealth += amount;
    }

    private IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        gameOver = true;
        GameOverUI.SetActive(true);
        if (!_played && _player.GetComponent<CaptainAnimAndSound>().endgame)
        {
            _audio.PlayOneShot(terminatedSFX, 1); // terminated tagline
            _played = true;
        }
    }
}