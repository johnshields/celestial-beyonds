using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Fader : MonoBehaviour
{
    private Animator _animator;
    private InputProfiler _controls;
    private int _fadeIn, _fadeOut;
    private bool _skip;
    public GameObject[] video;

    private void Awake()
    {
        _controls = new InputProfiler();
        _animator = GetComponent<Animator>();
        _fadeIn = Animator.StringToHash("FadeIn");
        _fadeOut = Animator.StringToHash("FadeOut");
    }

    private void Start()
    {
        _animator.SetBool(_fadeIn, true);
        _animator.SetBool(_fadeOut, false);
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

    private void SkipScene(InputAction.CallbackContext obj)
    {
        if (!_skip)
        {
            _skip = true;
            StartCoroutine(Fade(1));
        }
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
        
        yield return new WaitForSeconds(5);
        if (_skip) video[0].GetComponent<VideoPlayer>().Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}