using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject enemies, fader, skipBtn;
    public int time, skipInit, enemiesInit, fadeInit;
    public bool enableSkip, skip;
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
            fader.GetComponent<Animator>().SetBool("FadeIn", true);
            fader.GetComponent<Animator>().SetBool("FadeOut", false);   
        }

        if (time != 0)
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
            StartCoroutine(FadeSceneOut());
        }
    }

    private IEnumerator DisableSkip()
    {
        yield return new WaitForSeconds(5f);
        enableSkip = false;
        skipBtn.SetActive(false);
    }

    private IEnumerator SetActiveObjects()
    {
        yield return new WaitForSeconds(3);
        enemies.SetActive(true);
    }

    private IEnumerator FadeSceneOut()
    {
        if (!skip)
        {
            yield return new WaitForSeconds(time);
            fader.GetComponent<Animator>().SetBool($"FadeIn", false);
            fader.GetComponent<Animator>().SetBool($"FadeOut", true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            fader.GetComponent<Animator>().SetBool($"FadeIn", false);
            fader.GetComponent<Animator>().SetBool($"FadeOut", true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}