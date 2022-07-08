using System;
using UnityEngine;

public class BearSFX : MonoBehaviour
{
    public AudioClip[] enemySFX;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void BreathSound()
    {
        _audio.PlayOneShot(enemySFX[0]);
    }
    
    private void AttackSound()
    {
        _audio.PlayOneShot(enemySFX[1]);
    }
}
