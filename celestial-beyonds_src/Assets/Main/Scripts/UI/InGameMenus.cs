using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    private InputProfiler _controls;
    public GameObject pauseMenu, miniMenuPanel, fader, player, BtnGO, controlsPanel, muteBtn, unMuteBtn;
    public bool pausedActive, miniMenuActive, controlsMenu;
    public AudioSource audioToPause;
    public int audioPauseRequired;

    private void Awake()
    {
        pausedActive = false;
        pauseMenu.SetActive(false);
        _controls = new InputProfiler();
        
        if (!Booleans.muteActive)
        {
            unMuteBtn.SetActive(false);
            muteBtn.SetActive(true);
        }
        else
        {
            unMuteBtn.SetActive(true);
            muteBtn.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _controls.InGameUI.PauseGame.started += PauseGame;
        _controls.InGameUI.Resume.started += ResumeGame;
        _controls.InGameUI.LoadMainMenu.started += LoadMainMenu;
        _controls.InGameUI.MiniMenu.started += MiniMenu;
        _controls.UIActions.Controls.started += CtrlsMenu;
        _controls.UIActions.Back.started += BackBtn;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Enable();
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.PauseGame.started -= PauseGame;
        _controls.InGameUI.Resume.started -= ResumeGame;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.InGameUI.MiniMenu.started -= MiniMenu;
        _controls.UIActions.Controls.started -= CtrlsMenu;
        _controls.UIActions.Back.started -= BackBtn;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Disable();
        _controls.UIActions.Disable();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        if (!pausedActive)
        {
            pausedActive = true;
            pauseMenu.SetActive(true);
            print("Game paused: " + pausedActive);
            Time.timeScale = 0f;
            if(audioPauseRequired != 0)
                audioToPause.Pause();
        }
        else if (pausedActive)
        {
            pausedActive = false;
            pauseMenu.SetActive(false);
            controlsMenu = false;
            controlsPanel.SetActive(false);
            print("Game paused: " + pausedActive);
            Time.timeScale = 1f;
            if(audioPauseRequired != 0)
                audioToPause.UnPause();
        }
    }

    private void ResumeGame(InputAction.CallbackContext obj)
    {
        if (pausedActive && !player.GetComponent<CaptainHealth>().capDead)
        {
            pausedActive = false;
            pauseMenu.SetActive(false);
            controlsMenu = false;
            controlsPanel.SetActive(false);
            print("Game paused: " + pausedActive);
            Time.timeScale = 1f;
            if(audioPauseRequired != 0)
                audioToPause.UnPause();
        }
        else if (player.GetComponent<CaptainHealth>().gameOver)
        {
            StartCoroutine(GoLoadLevel(2));
        }
    }

    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        if (pausedActive || player.GetComponent<CaptainHealth>().gameOver)
        {
            pausedActive = false;
            StartCoroutine(GoLoadLevel(0));
        }
    }

    private void MiniMenu(InputAction.CallbackContext obj)
    {
        if (!miniMenuActive && !pausedActive && !player.GetComponent<CaptainHealth>().capDead)
        {
            miniMenuPanel.SetActive(true);
            miniMenuActive = true;
        }
        else
        {
            miniMenuPanel.SetActive(false);
            miniMenuActive = false;
        }
    }
    
    private void CtrlsMenu(InputAction.CallbackContext obj)
    {
        if (!controlsMenu && pausedActive)
        {
            print("Controls menu active:" + controlsMenu);
            controlsMenu = true;
            controlsPanel.SetActive(true);
        }
    }

    private void BackBtn(InputAction.CallbackContext obj)
    {
        if (controlsMenu && pausedActive)
        {
            print("Controls menu active: " + controlsMenu);
            controlsMenu = false;
            controlsPanel.SetActive(false);
        }
    }
    
    private void MuteGame(InputAction.CallbackContext obj)
    {
        if (pausedActive)
        {
            print("Mute Active: " +  Booleans.muteActive);
            Booleans.muteActive = true;
            muteBtn.SetActive(false);
            unMuteBtn.SetActive(true);
            AudioManager.MuteActive();   
        }
    }
    
    private void UnMuteGame(InputAction.CallbackContext obj)
    {
        if (pausedActive)
        {
            print("Mute Active: " +  Booleans.muteActive);
            Booleans.muteActive = false;
            muteBtn.SetActive(true);
            unMuteBtn.SetActive(false);
            AudioManager.MuteActive();   
        }
    }

    private IEnumerator GoLoadLevel(int level)
    {
        if(player.GetComponent<CaptainHealth>().gameOver)
            BtnGO.SetActive(false);
            
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool("FadeIn", false);
        fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}