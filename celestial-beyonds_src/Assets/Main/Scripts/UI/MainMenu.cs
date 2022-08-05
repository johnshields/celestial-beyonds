using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fader, actPanel, controlsPanel, creditsPanel, muteBtn, unMuteBtn, actBtn, restartBtn, restartPanel;
    private InputProfiler _controls;
    public bool controlsMenu, creditsRolling, loadPlanet;

    private void Awake()
    {
        if (PlayerMemory.sceneToLoad == "")
        {
            print("Players first time: " + true);
            PlayerMemory.sceneToLoad = "002_Opening";
        }
        else print("Players first time: " + false + "\nSceneToLoad: " + PlayerMemory.sceneToLoad);

        _controls = new InputProfiler();

        if (!Bools.muteActive)
        {
            unMuteBtn.SetActive(false);
            muteBtn.SetActive(true);
        }
        else
        {
            unMuteBtn.SetActive(true);
            muteBtn.SetActive(false);
        }

        if (PlayerMemory.sceneToLoad != "002_Opening")
        {
            ColorUtility.TryParseHtmlString("#2A2E4E", out var blue);
            restartBtn.GetComponent<Button>().interactable = true;
            restartBtn.GetComponent<Image>().color = blue;
        }
        else
        {
            restartBtn.GetComponent<Button>().interactable = false;
            restartBtn.GetComponent<Image>().color = Color.black;
        }

        if (PlayerMemory.sceneToLoad == "011_Earth")
        {
            ColorUtility.TryParseHtmlString("#2A2E4E", out var blue);
            actBtn.GetComponent<Button>().interactable = true;
            actBtn.GetComponent<Image>().color = blue;
        }
        else
        {
            actBtn.GetComponent<Button>().interactable = false;
            actBtn.GetComponent<Image>().color = Color.black;
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
        _controls.UIActions.RestartGame.started += RestartGame;
        _controls.UIActions.Yes.started += ConfirmYes;
        _controls.UIActions.No.started += ConfirmNo;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.UIActions.Credits.started += RollCredits;
        _controls.UIActions.LoadPlanet.started += LoadAct;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.Controls.started -= CtrlsMenu;
        _controls.UIActions.RestartGame.started -= RestartGame;
        _controls.UIActions.Yes.started -= ConfirmYes;
        _controls.UIActions.No.started -= ConfirmNo;
        _controls.UIActions.Mute.started -= MuteGame;
        _controls.UIActions.UnMute.started -= UnMuteGame;
        _controls.UIActions.Credits.started -= RollCredits;
        _controls.UIActions.LoadPlanet.started -= LoadAct;
        _controls.UIActions.Disable();
    }

    private void StartGame(InputAction.CallbackContext obj)
    {
        controlsMenu = false;
        controlsPanel.SetActive(false);
        StartCoroutine(LaunchGame(PlayerMemory.sceneToLoad));
    }

    private void RestartGame(InputAction.CallbackContext obj)
    {
        if (PlayerMemory.sceneToLoad != "002_Opening")
            restartPanel.SetActive(true);
    }
    
    private void ConfirmYes(InputAction.CallbackContext obj)
    {
        if (restartPanel.activeInHierarchy)
        {
            restartPanel.SetActive(false);
            PlayerMemory.ResetMemory();
            restartBtn.GetComponent<Button>().interactable = false;
            restartBtn.GetComponent<Image>().color = Color.black;
            actBtn.GetComponent<Button>().interactable = false;
            actBtn.GetComponent<Image>().color = Color.black;
            StartCoroutine(LaunchGame("002_Opening"));   
        }
    }

    private void ConfirmNo(InputAction.CallbackContext obj)
    {
        restartPanel.SetActive(false);
    }


    private void LoadAct(InputAction.CallbackContext obj)
    {
        if (PlayerMemory.sceneToLoad == "011_Earth")
        {
            if (!loadPlanet)
            {
                loadPlanet = true;
                print("loadPlanet menu active:" + loadPlanet);
                actPanel.SetActive(true);
            }
            else if (loadPlanet)
            {
                loadPlanet = false;
                print("loadPlanet menu active:" + loadPlanet);
                actPanel.SetActive(false);
            }   
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
        print("Mute Active: " + Bools.muteActive);
        Bools.muteActive = true;
        muteBtn.SetActive(false);
        unMuteBtn.SetActive(true);
        AudioManager.MuteActive();
    }

    private void UnMuteGame(InputAction.CallbackContext obj)
    {
        print("Mute Active: " + Bools.muteActive);
        Bools.muteActive = false;
        muteBtn.SetActive(true);
        unMuteBtn.SetActive(false);
        AudioManager.MuteActive();
    }


    private IEnumerator LaunchGame(string level)
    {
        PlayerPrefs.Save();

        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}