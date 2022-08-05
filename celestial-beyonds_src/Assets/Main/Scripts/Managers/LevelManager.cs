using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject enemies, fader, skipBtn, rainFader;
    public AudioSource cutSceneAudio;
    public int time, skipInit, enemiesInit, fadeInit;
    public bool enableSkip, skip, sceneAudioInit, rainFadeInit, credits;
    public string scene;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
        if(enemiesInit == 0)
            StartCoroutine(SetActiveObjects());
    }

    private void Start()
    {
        if (fadeInit == 0)
        {
            fader.GetComponent<Animator>().SetBool($"FadeIn", true);
            fader.GetComponent<Animator>().SetBool($"FadeOut", false);
        }
        
        if(credits)
            StartCoroutine(FadeSceneOut());
    }

    private void OnEnable()
    {
        _controls.Profiler.Skip.started += SkipScene;
        _controls.Profiler.EnableSkip.started += EnableSkip;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.Skip.started -= SkipScene;
        _controls.Profiler.EnableSkip.started -= EnableSkip;
        _controls.Profiler.Disable();
    }

    private void EnableSkip(InputAction.CallbackContext obj)
    {
        if (skipInit != 0 && !enableSkip)
        {
            enableSkip = true;
            skipBtn.SetActive(true);
            StartCoroutine(DisableSkip());
        }
    }

    private void SkipScene(InputAction.CallbackContext obj)
    {
        if (!skip && skipInit != 0 && enableSkip)
        {
            skip = true;
            skipBtn.SetActive(false);
            StartCoroutine(FadeSceneOut());
        }
    }

    private IEnumerator DisableSkip()
    {
        yield return new WaitForSeconds(3);
        enableSkip = false;
        skipBtn.SetActive(false);
    }

    private IEnumerator SetActiveObjects()
    {
        yield return new WaitForSeconds(3);
        enemies.SetActive(true);
    }

    public IEnumerator FadeSceneOut()
    {
        if (!skip)
        {
            yield return new WaitForSeconds(time);
            fader.GetComponent<Animator>().SetBool($"FadeIn", false);
            fader.GetComponent<Animator>().SetBool($"FadeOut", true);
            if(rainFadeInit)
                rainFader.GetComponent<RainFade>().RainFader(1);
            yield return new WaitForSeconds(4);
            if(!credits)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(scene);
        }
        else
        {
            if(sceneAudioInit)
                cutSceneAudio.Stop();
            fader.GetComponent<Animator>().SetBool($"FadeIn", false);
            fader.GetComponent<Animator>().SetBool($"FadeOut", true);
            if(rainFadeInit)
                rainFader.GetComponent<RainFade>().RainFader(1);
            yield return new WaitForSeconds(4);
            if(!credits)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(scene);
        }
    }
}