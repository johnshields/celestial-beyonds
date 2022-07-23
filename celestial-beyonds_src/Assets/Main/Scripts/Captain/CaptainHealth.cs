using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.UI;

public class CaptainHealth : MonoBehaviour
{
    public bool capDead, gameOver;
    public int maxHealth = 100, currentHealth;
    public GameObject pHealthBar, GameOverUI;
    private Slider _pHealthBarSlider;
    private GameObject _player;
    private bool _played;

    private void Start()
    {
        currentHealth = maxHealth;
        _pHealthBarSlider = pHealthBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _pHealthBarSlider.value = currentHealth;

        if (currentHealth >= maxHealth)
            currentHealth = 100;
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
        if (!_played)
        {
            //_audio.PlayOneShot(gameOverSFX, .2f); // terminated tagline
            _played = true;
        }
    }
}