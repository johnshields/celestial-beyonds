using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class CannonBlaster : MonoBehaviour
{
    public GameObject particles,
        canAmmo,
        cannon,
        pepperBox,
        celestialDefier;

    public GameObject[] pepperBoxParticles, celestialParticles;
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
        CallCannon();
    }

    public void HaltCannon()
    {
        status = 1;
        CallCannon();
    }

    private void CallCannon()
    {
        if (_player.GetComponent<CaptainAnimAndSound>().pepperBoxUpgrade)
            StartCoroutine(CannonWait());
        else if (_player.GetComponent<CaptainAnimAndSound>().celestialDefierUpgrade)
            StartCoroutine(CannonWait());
        else
            StartCoroutine(CannonWait());
    }

    private IEnumerator CannonWait()
    {
        if (!_player.GetComponent<CaptainAnimAndSound>().meleeActive && cannon.activeInHierarchy)
        {
            switch (status)
            {
                case 0:
                    yield return new WaitForSeconds(0.2f);
                    PlayCannonParticles();
                    break;
                case 1:
                    yield return new WaitForSeconds(0.5f);
                    StopCannonParticles();
                    break;
            }
        }
    }

    private void PlayCannonParticles()
    {
        if (pepperBox.activeInHierarchy && Booleans.pepperBoxUpgraded && !Booleans.celestialDeferUpgraded)
        {
            print("pepperBoxParticles");
            UpgradedParticle(1, 0);
        }
        else if (celestialDefier.activeInHierarchy && !Booleans.pepperBoxUpgraded && Booleans.celestialDeferUpgraded)
        {
            print("celestialParticles");
            UpgradedParticle(3, 0);
        }
        else if (!pepperBox.activeInHierarchy || !celestialDefier.activeInHierarchy 
                 && !Booleans.pepperBoxUpgraded && !Booleans.celestialDeferUpgraded)
        {
            particles.GetComponent<ParticleSystem>().Play();
        }
    }

    public void StopCannonParticles()
    {
        particles.GetComponent<ParticleSystem>().Stop();
        UpgradedParticle(0, 2);
        UpgradedParticle(0, 4);
    }

    private void UpgradedParticle(int turnOn, int turnOff)
    {
        if (turnOn == 1)
            for (var i = 0; i < 3; i++) pepperBoxParticles[i].GetComponent<ParticleSystem>().Play();
        if (turnOff == 2)
            for (var i = 0; i < 3; i++) pepperBoxParticles[i].GetComponent<ParticleSystem>().Stop();
        if (turnOn == 3)
            for (var i = 0; i < 5; i++) celestialParticles[i].GetComponent<ParticleSystem>().Play();
        if (turnOff == 4)
            for (var i = 0; i < 5; i++) celestialParticles[i].GetComponent<ParticleSystem>().Stop();
    }
}