using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private InputProfiler _controls;
    public GameObject pauseMenu, fader;
    private bool _paused;

    private void Awake()
    {
        _paused = false;
        pauseMenu.SetActive(false);
        _controls = new InputProfiler();
    }

    private void OnEnable()
    {
        _controls.InGameUI.PauseGame.started += PauseGame;
        _controls.InGameUI.Resume.started += ResumeGame;
        _controls.InGameUI.LoadMainMenu.started += LoadMainMenu;
        _controls.InGameUI.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.PauseGame.started -= PauseGame;
        _controls.InGameUI.Resume.started -= ResumeGame;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.InGameUI.Disable();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        _paused = true;
        pauseMenu.SetActive(true);
        print("Game paused...");
        Time.timeScale = 0f;
    }

    private void ResumeGame(InputAction.CallbackContext obj)
    {
        if (_paused)
        {
            _paused = false;
            pauseMenu.SetActive(false);
            print("Game resumed...");
            Time.timeScale = 1f;
        }
    }

    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        if (_paused)
        {
            _paused = false;
            StartCoroutine(GoMainMenu(0));
        }
    }

    private IEnumerator GoMainMenu(int level)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}