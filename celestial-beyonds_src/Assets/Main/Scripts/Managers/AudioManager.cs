using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static void MuteActive()
    {
        if (Booleans.muteActive)
            AudioListener.volume = 0f;
        else if (!Booleans.muteActive)
            AudioListener.volume = 1f;
        else
            Debug.LogWarning("No audio in scene.");
    }
}
