using System.Collections;
using Main.Scripts.UI.CursorControls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenus : MonoBehaviour
{
    private InputProfiler _controls;
    private GameObject _cursor, pl;
    public GameObject pauseMenu, miniMenuPanel, fader, player, BtnGO, controlsPanel, muteBtn, unMuteBtn, photoMode;
    public bool pausedActive, miniMenuActive, controlsMenu, cin;
    public AudioSource audioToPause;
    public int audioPauseRequired;
    public string reloadPlanet;
    public bool cursor;

    private void Awake()
    {
        _controls = new InputProfiler();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("PollinationLevel");
        pausedActive = false;
        pauseMenu.SetActive(false);
        Bools.cursorRequired = false;

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
        
        if (cursor)
            _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void OnEnable()
    {
        _controls.InGameUI.PauseGame.started += PauseGame;
        _controls.InGameUI.Resume.started += ResumeGame;
        _controls.InGameUI.Controls.started += CtrlsMenu;
        _controls.InGameUI.BackBtn.started += BackBtn;
        _controls.InGameUI.LoadMainMenu.started += LoadMainMenu;
        _controls.InGameUI.MiniMenu.started += MiniMenu;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Enable();
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.PauseGame.started -= PauseGame;
        _controls.InGameUI.Resume.started -= ResumeGame;
        _controls.InGameUI.Controls.started -= CtrlsMenu;
        _controls.InGameUI.BackBtn.started -= BackBtn;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.InGameUI.MiniMenu.started -= MiniMenu;
        _controls.UIActions.Mute.started += MuteGame;
        _controls.UIActions.UnMute.started += UnMuteGame;
        _controls.InGameUI.Disable();
        _controls.UIActions.Disable();
    }

    private void Update()
    {
        if (cursor)
        {
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "RestartPlanet")
                LoadMainMenuBtn();
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "MainMenuTerm")
                ReloadPlanetBtn();   
        }


        if (pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
            Time.timeScale = 0f;
        else if (pausedActive && photoMode.GetComponent<PhotoMode>().photoMode)
            Time.timeScale = 1f;
    }

    public void LoadMainMenuBtn()
    {
        StartCoroutine(GoLoadLevel("101_MainMenu"));
    }

    public void ReloadPlanetBtn()
    {
        StartCoroutine(GoLoadLevel(reloadPlanet));
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        if (!pausedActive)
        {
            pausedActive = true;
            pauseMenu.SetActive(true);
            print("Game paused: " + pausedActive);
            Time.timeScale = 0f;
            if (audioPauseRequired != 0) audioToPause.Pause();
            Bools.cursorRequired = true;
            Cursor.visible = true;
        }
        else if (pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            pausedActive = false;
            pauseMenu.SetActive(false);
            controlsMenu = false;
            controlsPanel.SetActive(false);
            print("Game paused: " + pausedActive);
            Time.timeScale = 1f;
            if (audioPauseRequired != 0) audioToPause.UnPause();
            Bools.cursorRequired = false;
            Cursor.visible = false;
        }
    }

    private void ResumeGame(InputAction.CallbackContext obj)
    {
        if (pausedActive && !player.GetComponent<CaptainHealth>().capDead &&
            !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            pausedActive = false;
            pauseMenu.SetActive(false);
            controlsMenu = false;
            controlsPanel.SetActive(false);
            print("Game paused: " + pausedActive);
            Time.timeScale = 1f;
            if (audioPauseRequired != 0)
                audioToPause.UnPause();
            Bools.cursorRequired = false;
            Cursor.visible = false;
        }
        else if (player.GetComponent<CaptainHealth>().gameOver)
        {
            StartCoroutine(GoLoadLevel(reloadPlanet));
        }
    }

    // Input System
    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        if (pausedActive || player.GetComponent<CaptainHealth>().gameOver)
        {
            pausedActive = false;
            StartCoroutine(GoLoadLevel("101_MainMenu"));
            if (pl.GetComponent<PollinationLevel>().pollinationPercent != 100)
            {
                PlayerMemory.peridots -= Peridots.peridotsCollectedInLvl;
                Peridots.peridotsCollectedInLvl = 0;   
            }
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
        if (!controlsMenu && pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            print("Controls menu active:" + controlsMenu);
            controlsMenu = true;
            controlsPanel.SetActive(true);
        }
    }

    private void BackBtn(InputAction.CallbackContext obj)
    {
        if (controlsMenu && pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            print("Controls menu active: " + controlsMenu);
            controlsMenu = false;
            controlsPanel.SetActive(false);
        }
    }

    private void MuteGame(InputAction.CallbackContext obj)
    {
        if (pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            print("Mute Active: " + Bools.muteActive);
            Bools.muteActive = true;
            muteBtn.SetActive(false);
            unMuteBtn.SetActive(true);
            AudioManager.MuteActive();
        }
    }

    private void UnMuteGame(InputAction.CallbackContext obj)
    {
        if (pausedActive && !photoMode.GetComponent<PhotoMode>().photoMode)
        {
            print("Mute Active: " + Bools.muteActive);
            Bools.muteActive = false;
            muteBtn.SetActive(true);
            unMuteBtn.SetActive(false);
            AudioManager.MuteActive();
        }
    }

    private IEnumerator GoLoadLevel(string level)
    {
        ClickedElementEmpty();
        if (player.GetComponent<CaptainHealth>().gameOver)
            BtnGO.SetActive(false);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        print("Loading: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        if (!cin)
        {
            if(player.GetComponent<CaptainHealth>().gameOver)
                player.GetComponent<CaptainHealth>().ResetPeridots();   
        }
        SceneManager.LoadScene(level);
    }
    
    private void ClickedElementEmpty()
    {
        if (cursor)
            _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
    }
}