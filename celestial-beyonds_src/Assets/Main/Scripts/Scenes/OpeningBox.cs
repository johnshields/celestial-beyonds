using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpeningBox : MonoBehaviour
{
    public GameObject musicAudio, video;
    public float syncTime = 0.4f;
    public GameObject _fader;

    private void Awake()
    {
        StartCoroutine(SyncAudioAndVideo());
    }

    private void Start()
    {
        video.GetComponent<VideoPlayer>().Play();
        musicAudio.GetComponent<AudioSource>().Play();
        StartCoroutine(OpeningOver());
    }

    private IEnumerator SyncAudioAndVideo()
    {
        video.GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(syncTime);
        musicAudio.GetComponent<AudioSource>().Play();
    }

    private IEnumerator OpeningOver()
    {
        yield return new WaitForSeconds(135f); // length of opening
        _fader.GetComponent<Animator>().SetBool("FadeIn", false);
        _fader.GetComponent<Animator>().SetBool("FadeOut", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}