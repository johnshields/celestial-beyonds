using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.UI;

public class CaptainHealth : MonoBehaviour
{
    public static bool capDead;
    public int maxHealth = 100, currentHealth;
    public GameObject pHealthBar;
    private Slider _pHealthBarSlider;
    private GameObject _player;

    private void Start()
    {
        currentHealth = maxHealth;
        _pHealthBarSlider = pHealthBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _pHealthBarSlider.value = currentHealth;
    }

    public void PlayerTakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            capDead = true;
            print("player dead!");
            _player.GetComponent<CaptainAnimAndSound>().CapDeath();
        }
    }

    public void PlayerGainHealth(int amount)
    {
        if (currentHealth != maxHealth)
            currentHealth += amount;
    }
}