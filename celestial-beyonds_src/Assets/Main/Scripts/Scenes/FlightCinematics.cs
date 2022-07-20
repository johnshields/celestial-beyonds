using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FlightCinematics : MonoBehaviour
{
    public GameObject musicAudio, video;
    public float syncTime = 0.4f, vidLenght;
    public GameObject _fader, cinControls;
    private InputProfiler _controls;

    private void Awake()
    {
        Time.timeScale = 0;
        _controls = new InputProfiler();
    }

    private void Start()
    {
        StartCoroutine(CinematicOver());
    }

    private void OnEnable()
    {
        _controls.UIActions.PlayCinematic.started += PlayCinematic;
        _controls.UIActions.PlayCinematic.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.PlayCinematic.started -= PlayCinematic;
        _controls.UIActions.PlayCinematic.Disable();
    }

    private void PlayCinematic(InputAction.CallbackContext obj)
    {
        Time.timeScale = 1;
        cinControls.SetActive(false);
        StartCoroutine(SyncCin());
    }

    private IEnumerator SyncCin()
    {
        video.GetComponent<VideoPlayer>().Play();
        yield return new WaitForSeconds(syncTime);
        musicAudio.GetComponent<AudioSource>().Play();
    }


    private IEnumerator CinematicOver()
    {
        yield return new WaitForSeconds(vidLenght); // length of cinematic
        video.GetComponent<VideoPlayer>().Stop();
        _fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        _fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}