using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.UI;

public class CaptainHealth : MonoBehaviour
{
    public int maxHealth = 100, currentHealth;
    public GameObject pHealthBar;
    private Slider _pHealthBarSlider;
    private GameObject _player;
    public static bool capDead;

    private void Start()
    {
        currentHealth = maxHealth;
        _pHealthBarSlider = pHealthBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerTakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            print("player dead!");
            _player.GetComponent<CaptainAnimAndSound>().CapDeath();
            capDead = true;
        }
    }

    public void PlayerGainHealth(int amount)
    {
        if (currentHealth != maxHealth)
            currentHealth += amount;
    }

    private void Update()
    {
        _pHealthBarSlider.value = currentHealth;
    }
}