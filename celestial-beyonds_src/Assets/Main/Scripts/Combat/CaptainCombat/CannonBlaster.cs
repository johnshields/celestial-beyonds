using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class CannonBlaster : MonoBehaviour
{
    public GameObject particles, canAmmo;
    private GameObject _player;
    private int status;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void FireCannon()
    {
        status = 0;
        canAmmo.GetComponent<CannonAmmo>().cannonAmmo -= 10;
        StartCoroutine(CannonWait());
    }

    public void HaltCannon()
    {
        status = 1;
        if (!_player.GetComponent<CaptainAnimAndSound>().meleeActive)
            StartCoroutine(CannonWait());
    }

    public void StopCannonParticles()
    {
        particles.GetComponent<ParticleSystem>().Stop();
    }


    private IEnumerator CannonWait()
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