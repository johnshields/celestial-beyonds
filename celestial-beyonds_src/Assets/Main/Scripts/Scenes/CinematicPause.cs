using UnityEngine;
using UnityEngine.InputSystem;

public class CinematicPause : MonoBehaviour
{
    private InputProfiler _controls;
    public bool pausedCinActive;
    public GameObject pauseMenu, muteBtn, unMuteBtn;
    public AudioSource cinMusic;
    
    private void Awake()
    {
        _controls = new InputProfiler();
        pausedCinActive = false;
        
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
        _controls.InGameUI.LoadMainMenu.started += LoadMainMenu;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Enable();
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.PauseGame.started -= PauseGame;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Disable();
        _controls.UIActions.Disable();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        if (!pausedCinActive)
        {
            print("Game paused: " + pausedCinActive);
            cinMusic.Pause();
            pausedCinActive = true;   
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            print("Game paused: " + pausedCinActive);
            cinMusic.UnPause();
            pausedCinActive = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    private void MuteGame(InputAction.CallbackContext obj)
    {
        if (pausedCinActive)
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
        if (pausedCinActive)
        {
            print("Mute Active: " +  Booleans.muteActive);
            Booleans.muteActive = false;
            muteBtn.SetActive(true);
            unMuteBtn.SetActive(false);
            AudioManager.MuteActive();   
        }
    }

    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        print("LoadMainMenu");
    }
}
