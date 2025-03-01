using UnityEngine;

public class AristauesSFX : MonoBehaviour
{
    public AudioClip footstepSFX, fallSFX;
    public GameObject randoAudio;
    private AudioSource _audio;
    private bool _tlPlayed, aPlayed;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        PlayRandomClip("Taglines", 0f);
        PlayRandomClip("Triton", 0f);
    }

    private void FootstepSFX()
    {
        _audio.PlayOneShot(footstepSFX, 0.1f);
    }

    private void TaglinesSFX()
    {
        if (!_tlPlayed)
        {
            _tlPlayed = true;
            PlayRandomClip("Taglines", 0.5f);
            Invoke(nameof(ResetTaglineSFX), 6);
        }
    }

    private void AttackSFX()
    {
        if (!aPlayed)
        {
            aPlayed = true;
            PlayRandomClip("Triton", .6f);
            Invoke(nameof(ResetAttackSFX), 3);
        }
    }

    private void FallSFX()
    {
        _audio.Stop();
        _audio.PlayOneShot( fallSFX, 0.8f);
    }

    private void ResetAttackSFX()
    {
        aPlayed = false;
    }
    
    private void ResetTaglineSFX()
    {
        _tlPlayed = false;
    }
    
    private void PlayRandomClip( string path, float vol)
    {
        _audio.PlayOneShot(randoAudio.GetComponent<AudioRandomizer>().GetRandomClip("Aristaues/SFX/" + path), vol);
    }
}
