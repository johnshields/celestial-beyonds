using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Fader : MonoBehaviour
{
    public GameObject musicAudio, video;
    public float syncTime;
    private Animator _animator;
    private int _fadeIn, _fadeOut;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
        _animator = GetComponent<Animator>();
        _fadeIn = Animator.StringToHash("FadeIn");
        _fadeOut = Animator.StringToHash("FadeOut");

        _animator.SetBool(_fadeIn, true);
        _animator.SetBool(_fadeOut, false);
        StartCoroutine(SyncAudioAndVideo());
    }

    private void OnEnable()
    {
        _controls.Profiler.Skip.started += SkipScene;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.Skip.started -= SkipScene;
        _controls.Profiler.Disable();
    }

    private IEnumerator SyncAudioAndVideo()
    {
        musicAudio.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(syncTime);
        video.GetComponent<VideoPlayer>().Play();
    }

    private void SkipScene(InputAction.CallbackContext obj)
    {
        StartCoroutine(Fade(1));
    }

    private IEnumerator Fade(int fade)
    {
        switch (fade)
        {
            case 0:
                _animator.SetBool(_fadeIn, true);
                _animator.SetBool(_fadeOut, false);
                break;
            case 1:
                _animator.SetBool(_fadeIn, false);
                _animator.SetBool(_fadeOut, true);
                break;
        }

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}