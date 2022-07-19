using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fader, controlsPanel, creditsPanel, muteBtn, unMuteBtn;
    private InputProfiler _controls;
    public bool controlsMenu, creditsRolling;

    private void Awake()
    {
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

    private void Start()
    {
        fader.GetComponent<Animator>().SetBool($"FadeIn", true);
        fader.GetComponent<Animator>().SetBool($"FadeOut", false);
    }

    private void OnEnable()
    {
        _controls.UIActions.StartGame.started += StartGame;
        _controls.UIActions.Controls.started += CtrlsMenu;
        _controls.UIActions.Back.started += BackBtn;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.UIActions.Credits.started += RollCredits;
        _controls.UIActions.QuitGame.started += QuitGame;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.Controls.started -= CtrlsMenu;
        _controls.UIActions.Back.started -= BackBtn;
        _controls.UIActions.Mute.started -= MuteGame;
        _controls.UIActions.UnMute.started -= UnMuteGame;
        _controls.UIActions.Credits.started -= RollCredits;
        _controls.UIActions.QuitGame.started -= QuitGame;
        _controls.UIActions.Disable();
    }

    // ControllerInput
    private void StartGame(InputAction.CallbackContext obj)
    {
        controlsMenu = false;
        controlsPanel.SetActive(false);
        StartCoroutine(LaunchGame(1));
    }
    
    // MouseUI Input
    public void StartGameM()
    {
        controlsMenu = false;
        controlsPanel.SetActive(false);
        StartCoroutine(LaunchGame(1));
    }

    private void CtrlsMenu(InputAction.CallbackContext obj)
    {
        if (!controlsMenu)
        {
            print("Controls menu active:" + controlsMenu);
            controlsMenu = true;
            controlsPanel.SetActive(true);
        }
    }
    
    private void RollCredits(InputAction.CallbackContext obj)
    {
        if (!creditsRolling)
        {
            creditsRolling = true;
            creditsPanel.SetActive(true);
        }
        else if (creditsRolling)
        {
            creditsRolling = false;
            creditsPanel.SetActive(false);
        }
    }

    private void BackBtn(InputAction.CallbackContext obj)
    {
        if (controlsMenu)
        {
            print("Controls menu active: " + controlsMenu);
            controlsMenu = false;
            controlsPanel.SetActive(false);
        }
    }
    
    private void MuteGame(InputAction.CallbackContext obj)
    {
        print("Mute Active: " +  Booleans.muteActive);
        Booleans.muteActive = true;
        muteBtn.SetActive(false);
        unMuteBtn.SetActive(true);
        AudioManager.MuteActive();
    }
    
    private void UnMuteGame(InputAction.CallbackContext obj)
    {
        print("Mute Active: " +  Booleans.muteActive);
        Booleans.muteActive = false;
        muteBtn.SetActive(true);
        unMuteBtn.SetActive(false);
        AudioManager.MuteActive();
    }

    private void QuitGame(InputAction.CallbackContext obj)
    {
        print("Quiting Game...");
        StartCoroutine(FadeAndQuitGame());
    }


    private IEnumerator LaunchGame(int level)
    {
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool("FadeIn", false);
        fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }

    private IEnumerator FadeAndQuitGame()
    {
        fader.GetComponent<Animator>().SetBool("FadeIn", false);
        fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
           Application.Quit();
#endif
    }
}