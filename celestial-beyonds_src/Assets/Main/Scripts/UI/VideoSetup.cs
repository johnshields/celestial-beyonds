using UnityEngine;
using UnityEngine.Video;

public class VideoSetup : MonoBehaviour
{
    public GameObject videoPlayer;
    public string videoURL;

    private void Awake()
    {
        videoPlayer.GetComponent<VideoPlayer>().url = videoURL;
    }
}