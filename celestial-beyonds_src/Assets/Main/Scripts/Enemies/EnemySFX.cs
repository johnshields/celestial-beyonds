using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    public AudioClip[] enemySFX;
    private AudioSource _audio;
    private bool _played;

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
        if (!_played)
        {
            _played = true;
            _audio.PlayOneShot(enemySFX[1]);
            Invoke(nameof(ResetSFX), 3);
        }
    }

    private void ResetSFX()
    {
        _played = false;
    }
}
