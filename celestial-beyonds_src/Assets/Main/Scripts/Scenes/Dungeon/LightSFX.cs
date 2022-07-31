using UnityEngine;

public class LightSFX : MonoBehaviour
{
    public AudioClip lightSFX;
    private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void LightWorkingSFX()
    {
        _audio.PlayOneShot(lightSFX);
    }
}
