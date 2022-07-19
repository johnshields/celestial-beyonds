using System.Collections;
using UnityEngine;

public class OpeningBox : MonoBehaviour
{
    public GameObject musicAudio;
    public float syncTime = 0.4f;

    private void Awake()
    {
        StartCoroutine(SyncMusic());
    }

    private IEnumerator SyncMusic()
    {
        yield return new WaitForSeconds(syncTime);
        musicAudio.GetComponent<AudioSource>().Play();
    }
}