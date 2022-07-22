using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class Pollinator : MonoBehaviour
{
    public GameObject particles, ammo, pollinator;
    private GameObject _player;
    private int status;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void FirePollinator()
    {
        status = 0;
        ammo.GetComponent<PollinatorAmmo>().pollenAmmo -= 10;
        StartCoroutine(PollinatorWait());
    }

    public void HaltPollinator()
    {
        status = 1;
        if (!_player.GetComponent<CaptainAnimAndSound>().meleeActive  
            && pollinator.activeInHierarchy)
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