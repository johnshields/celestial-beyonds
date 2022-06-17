using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    private Animator _animator;
    private int _fadeIn, _fadeOut;
    private InputProfiler _controls;

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

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}