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
    private GameObject _player;
    private bool peridotsReset;

    private void Start()
    {
        currentHealth = maxHealth;
        pHealthBarSlider = pHealthBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
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
        if (!capDead)
            currentHealth -= amount;

        if (currentHealth <= 0)
        {
            capDead = true;
            print("Player Terminated!");
            if (!peridotsReset)
            {
                peridotsReset = true;
                PlayerMemory.peridots -= Peridots.peridotsCollectedInLvl;
            }

            _player.GetComponent<CaptainAnimAndSound>().CapDeath();
            StartCoroutine(GameOverScreen());
        }
    }

    public void ResetPeridots()
    {
        Peridots.peridotsCollectedInLvl = 0;
        peridotsReset = false;
    }

    public void PlayerGainHealth(int amount)
    {
        if (currentHealth != maxHealth)
            currentHealth += amount;
    }

    private IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("ControllerCursor/Controller/Cursor").SetActive(true);
        GameObject.Find("ControllerCursor/Controller").GetComponent<Rigidbody2D>().constraints =
            RigidbodyConstraints2D.None;
        Cursor.visible = true;
        gameOver = true;
        GameOverUI.SetActive(true);
    }
}