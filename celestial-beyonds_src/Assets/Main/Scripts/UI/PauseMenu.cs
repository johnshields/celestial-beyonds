using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private InputProfiler _controls;
    public GameObject pauseMenu, miniMenu, fader, player, BtnGO;
    private bool _paused, _miniMenu;

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
        _controls.InGameUI.MiniMenu.started += MiniMenu;
        _controls.InGameUI.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.PauseGame.started -= PauseGame;
        _controls.InGameUI.Resume.started -= ResumeGame;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.InGameUI.MiniMenu.started -= MiniMenu;
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
        if (_paused && !player.GetComponent<CaptainHealth>().capDead)
        {
            _paused = false;
            pauseMenu.SetActive(false);
            print("Game resumed...");
            Time.timeScale = 1f;
        }
        else if (player.GetComponent<CaptainHealth>().gameOver)
        {
            StartCoroutine(GoLoadLevel(3));
        }
    }

    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        if (_paused || player.GetComponent<CaptainHealth>().gameOver)
        {
            _paused = false;
            StartCoroutine(GoLoadLevel(0));
        }
    }

    private void MiniMenu(InputAction.CallbackContext obj)
    {
        if (!_miniMenu && !player.GetComponent<CaptainHealth>().capDead)
        {
            miniMenu.SetActive(true);
            _miniMenu = true;
        }
        else
        {
            miniMenu.SetActive(false);
            _miniMenu = false;
        }
    }

    private IEnumerator GoLoadLevel(int level)
    {
        if(player.GetComponent<CaptainHealth>().gameOver)
            BtnGO.SetActive(false);
            
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}