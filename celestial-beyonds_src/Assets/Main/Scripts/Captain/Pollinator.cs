using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pollinator : MonoBehaviour
{
    public GameObject particles, pollenBar;
    private int status;
    public int maxAmmo = 5;
    public static int pollenAmmo;
    private Slider _pollenBarSlider;

    private void Start()
    {
        pollenAmmo = maxAmmo;
        _pollenBarSlider = pollenBar.GetComponent<Slider>();
    }

    private void Update()
    {
        _pollenBarSlider.value = pollenAmmo;
    }

    public void FirePollinator()
    {
        status = 0;
        pollenAmmo -= 50;
        StartCoroutine(PollinatorWait());
    }

    public void HaltPollinator()
    {
        status = 1;
        StartCoroutine(PollinatorWait());
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
