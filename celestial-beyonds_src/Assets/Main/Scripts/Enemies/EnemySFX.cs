using System;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public AudioClip[] enemySFX;
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void PatrolSFX()
    {
        _audio.PlayOneShot(enemySFX[0]);
    }
    
    private void AttackSFX()
    {
        _audio.PlayOneShot(enemySFX[1]);
    }
}
