using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.UI;

public class Pollinator : MonoBehaviour
{
    public GameObject particles, pollenBar;
    public int maxAmmo = 500;
    public int pollenAmmo;
    private GameObject _player;
    private Slider _pollenBarSlider;
    private int status;

    private void Start()
    {
        pollenAmmo = maxAmmo;
        _pollenBarSlider = pollenBar.GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        _pollenBarSlider.value = pollenAmmo;
    }

    public void FillUpPollen(int amount)
    {
        pollenAmmo += amount;
    }

    public void FirePollinator()
    {
        status = 0;
        pollenAmmo -= 10;
        StartCoroutine(PollinatorWait());
    }

    public void HaltPollinator()
    {
        status = 1;
        if (!_player.GetComponent<CaptainAnimAndSound>().meleeActive)
            StartCoroutine(PollinatorWait());
    }

    public void StopPollenParticles()
    {
        particles.GetComponent<ParticleSystem>().Stop();
    }

    private IEnumerator PollinatorWait()
    {
        switch (status)
        {
            case 0:
                yield return new WaitForSeconds(0.2f);
                particles.GetComponent<ParticleSystem>().Play();
                break;
            case 1:
                yield return new WaitForSeconds(0.5f);
                particles.GetComponent<ParticleSystem>().Stop();
                break;
        }
    }
}