using System;
using System.Collections;
using Main.Scripts.UI.CursorControls;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fader,
        actPanel,
        controlsPanel,
        creditsPanel,
        muteBtn,
        unMuteBtn,
        actBtn,
        restartBtn,
        restartPanel;

    public TextMeshProUGUI ActsLockedMsg, restartLockedMsg, currentGameTxt;
    public RawImage sceneImg;
    public Texture[] sceneImgToChange;
    public bool controlsMenu, creditsRolling, loadActs;
    private bool _launchGameEnabled, _restartEnabled, _alertedRestartMsg, _alertedActsMsg;
    private string _currentScene;
    private InputProfiler _controls;
    private GameObject _cursor;
    private bool calledTest1, calledTest2, calledTest3;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _launchGameEnabled = true;
        _restartEnabled = true;
        _alertedRestartMsg = false;
        _alertedActsMsg = false;
        Bools.cursorRequired = true;

        if (PlayerMemory.sceneToLoad == string.Empty)
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
        _cursor = GameObject.FindWithTag("Cursor");
        fader.GetComponent<Animator>().SetBool($"FadeIn", true);
        fader.GetComponent<Animator>().SetBool($"FadeOut", false);
        
       AudioManager.MuteActive();
    }

    private void OnEnable()
    {
        _controls.UIActions.LaunchGameBtn.started += LaunchGameInput;
        _controls.UIActions.Restart.started += RestartGameInput;
        _controls.UIActions.ConfirmYes.started += ConfirmYesInput;
        _controls.UIActions.ConfirmNo.started += ConfirmNoInput;
        _controls.UIActions.QuitGame.started += QuitGameInput;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.LaunchGameBtn.started -= LaunchGameInput;
        _controls.UIActions.Restart.started -= RestartGameInput;
        _controls.UIActions.ConfirmYes.started -= ConfirmYesInput;
        _controls.UIActions.ConfirmNo.started -= ConfirmNoInput;
        _controls.UIActions.QuitGame.started -= QuitGameInput;
        _controls.UIActions.Mute.started -= MuteGame;
        _controls.UIActions.UnMute.started -= UnMuteGame;
        _controls.UIActions.Disable();
    }

    private void OnGUI()
    {
        switch (PlayerMemory.sceneToLoad)
        {
            case "":
                _currentScene = "Opening";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[0];
                break;
            case "002_Opening":
                _currentScene = "Opening";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[0];
                break;
            case "003_CelestialWaltz":
                _currentScene = "Celestial Waltz";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[1];
                break;
            case "004_Intro_TRAPPIST-1":
                _currentScene = "TRAPPIST-1 Intro";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[2];
                break;
            case "004_TRAPPIST-1":
                _currentScene = "TRAPPIST-1";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[3];
                break;
            case "005_LunarPulse":
                _currentScene = "Lunar Pulse";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[4];
                break;
            case "006_Intro_PCB":
                _currentScene = "Proxima Centauri B Intro";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[5];
                break;
            case "006_ProximaCentauriB":
                _currentScene = "Proxima Centauri B";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[6];
                break;
            case "007_Man_of_Celestial_Man_of_Faith":
                _currentScene = "Man of Celestial Man of Faith";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[7];
                break;
            case "008_IntroKepler-186f":
                _currentScene = "Kepler-186f Intro";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[8];
                break;
            case "008_Kepler-186f":
                _currentScene = "Kepler-186f";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[9];
                break;
            case "009_Intro_Endgame":
                _currentScene = "Endgame Intro";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[10];
                break;
            case "009_Endgame":
                _currentScene = "Endgame";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[11];
                break;
            case "010_TheGoldenRecord":
                _currentScene = "The Golden Record";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[12];
                break;
            case "011_Earth":
                _currentScene = "Earth";
                sceneImg.GetComponent<RawImage>().texture = sceneImgToChange[13];
                break;
        }

        currentGameTxt.text = _currentScene;
    }

    private void Update()
    {
        // Main Buttons
        if (_cursor.GetComponent<ControllerCursor>().clickedElement == "LaunchBtn")
            LaunchGame();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Restart")
            RestartGame();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "LoadAct")
            LoadAct();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Controls")
            CtrlsMenu();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Credits")
            RollCredits();
        
        // Sub Buttons - Close
        if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseActs")
            CloseActs();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseControls")
            CloseControls();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseCredits")
            CloseCredits();
        
        if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ConfirmYes")
            ConfirmYes();
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ConfirmNo")
            ConfirmNo();
    }
    
    // Input System
    private void LaunchGameInput(InputAction.CallbackContext obj)
    {
        LaunchGame();
    }
    
    private void RestartGameInput(InputAction.CallbackContext obj)
    {
        RestartGame();
    }

    private void ConfirmYesInput(InputAction.CallbackContext obj)
    {
       ConfirmYes();
    }

    private void ConfirmNoInput(InputAction.CallbackContext obj)
    {
        ConfirmNo();
    }

    // Cursor
    public void LaunchGame()
    {
        ClickedElementEmpty();
        if (_launchGameEnabled && !actPanel.activeInHierarchy)
            StartCoroutine(LaunchGame(PlayerMemory.sceneToLoad));
    }

    public void RestartGame()
    {
        ClickedElementEmpty();
        if (PlayerMemory.sceneToLoad == "002_Opening" && !_alertedRestartMsg)
        {
            _alertedRestartMsg = true;
            restartLockedMsg.text = "Saved Progress == Null!";
            StartCoroutine(TurnOffLockedMsg(restartLockedMsg));
        }
        if (PlayerMemory.sceneToLoad != "002_Opening")
            if (_restartEnabled  && !actPanel.activeInHierarchy && !restartPanel.activeInHierarchy)
                restartPanel.SetActive(true);
    }

    public void ConfirmYes()
    {
        ClickedElementEmpty();
        if (restartPanel.activeInHierarchy)
        {
            _launchGameEnabled = false;
            _restartEnabled = false;
            restartPanel.SetActive(false);
            PlayerMemory.ResetMemory();
            restartBtn.GetComponent<Button>().interactable = false;
            restartBtn.GetComponent<Image>().color = Color.black;
            actBtn.GetComponent<Button>().interactable = false;
            actBtn.GetComponent<Image>().color = Color.black;
            StartCoroutine(LaunchGame("002_Opening"));
        }
    }

    public void ConfirmNo()
    {
        ClickedElementEmpty();
        if (restartPanel.activeInHierarchy)
        {
            restartPanel.SetActive(false);
            _restartEnabled = true;
            _launchGameEnabled = true;
        }
    }


    public void LoadAct()
    {
        ClickedElementEmpty();
        if (PlayerMemory.sceneToLoad != "011_Earth" && !_alertedActsMsg)
        {
            _alertedActsMsg = true;
            ActsLockedMsg.text = "All Acts must be Completed to Unlock the Act Loader!";
            StartCoroutine(TurnOffLockedMsg(ActsLockedMsg));
        }

        if (PlayerMemory.sceneToLoad == "011_Earth")
        {
            if (!loadActs)
            {
                _launchGameEnabled = false;
                _restartEnabled = false;
                loadActs = true;
                print("loadPlanet menu active:" + loadActs);
                Panels(true, false, false);
            }
        }
    }
    
    public void CloseActs()
    {
        ClickedElementEmpty();
        if (loadActs)
        {
            _launchGameEnabled = true;
            _restartEnabled = true;
            loadActs = false;
            print("loadPlanet menu active:" + loadActs);
            actPanel.SetActive(false);
        }
    }

    private IEnumerator TurnOffLockedMsg(TMP_Text txt)
    {
        yield return new WaitForSeconds(3);
        txt.text = string.Empty;
    }

    public void CtrlsMenu()
    {
        ClickedElementEmpty();
        if (!controlsMenu)
        {
            _launchGameEnabled = false;
            _restartEnabled = false;
            controlsMenu = true;
            Panels(false, true, false);
        }
    }
    
        
    public void CloseControls()
    {
        ClickedElementEmpty();
        if (controlsMenu)
        {
            _launchGameEnabled = true;
            _restartEnabled = true;
            controlsMenu = false;
            print("Controls menu active:" + controlsMenu);
            controlsPanel.SetActive(false);
        }
    }

    public void RollCredits()
    {
        ClickedElementEmpty();
        if (!creditsRolling)
        {
            _launchGameEnabled = false;
            _restartEnabled = false;
            creditsRolling = true;
            Panels(false, false, true);
            creditsPanel.GetComponentInChildren<Animator>().SetBool($"CreditsRoll", true);
            StartCoroutine(EndCredits());
        }
    }
    
    public void CloseCredits()
    {
        ClickedElementEmpty();
        if (creditsRolling)
        {
            _launchGameEnabled = true;
            _restartEnabled = true;
            creditsRolling = false;
            creditsPanel.SetActive(false);
            creditsPanel.GetComponentInChildren<Animator>().SetBool($"CreditsRoll", false);
            StopCoroutine(EndCredits());
        }
    }

    private void Panels(bool acts, bool ctrls, bool credits)
    {
        actPanel.SetActive(acts);
        controlsPanel.SetActive(ctrls);
        creditsPanel.SetActive(credits);
        restartPanel.SetActive(false);
        loadActs = acts;
        controlsMenu = ctrls;
        creditsRolling = credits;
    }
    
    // Editor and Windows Build (Esc Key)
    private void QuitGameInput(InputAction.CallbackContext obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
           Application.Quit();
#endif
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
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }

    private IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(90f);
        creditsRolling = false;
        creditsPanel.SetActive(false);
        creditsPanel.GetComponentInChildren<Animator>().SetBool($"CreditsRoll", false);
    }

    private void ClickedElementEmpty()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
    }
}