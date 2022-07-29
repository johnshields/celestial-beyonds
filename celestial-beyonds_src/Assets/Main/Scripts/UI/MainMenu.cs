using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fader, actPanel, controlsPanel, creditsPanel, muteBtn, unMuteBtn;
    private InputProfiler _controls;
    public bool controlsMenu, creditsRolling, loadPlanet;

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
        _controls.UIActions.LoadPlanet.started += LoadPlanet;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.UIActions.Credits.started += RollCredits;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.Controls.started -= CtrlsMenu;
        _controls.UIActions.LoadPlanet.started -= LoadPlanet;
        _controls.UIActions.Mute.started -= MuteGame;
        _controls.UIActions.UnMute.started -= UnMuteGame;
        _controls.UIActions.Credits.started -= RollCredits;
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

    private void LoadPlanet(InputAction.CallbackContext obj)
    {
        if (!loadPlanet)
        {
            loadPlanet = true;
            print("loadPlanet menu active:" + loadPlanet);
            actPanel.SetActive(true);
        }
        else if(loadPlanet)
        {
            loadPlanet = false;
            print("loadPlanet menu active:" + loadPlanet);
            actPanel.SetActive(false);
        }
    }

    private void CtrlsMenu(InputAction.CallbackContext obj)
    {
        if (!controlsMenu && !loadPlanet)
        {
            controlsMenu = true;
            print("Controls menu active:" + controlsMenu);
            controlsPanel.SetActive(true);
        }
        else if (controlsMenu)
        {
            controlsMenu = false;
            print("Controls menu active:" + controlsMenu);
            controlsPanel.SetActive(false);
        }
    }
    
    private void RollCredits(InputAction.CallbackContext obj)
    {
        if (!creditsRolling && !loadPlanet)
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


    private IEnumerator LaunchGame(int level)
    {
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}