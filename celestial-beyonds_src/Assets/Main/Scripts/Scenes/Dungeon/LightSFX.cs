using UnityEngine;

public class LightSFX : MonoBehaviour
{
    public AudioClip lightSFX;
    private AudioSource _audio;
    
   private void Start()
    {
        _audio = GetComponent<AudioSource>();
        print($"LightSFX {_audio} assigned {true}");
    }

    private void LightWorkingSFX()
    {
        _audio.PlayOneShot(lightSFX);
    }
}
