using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PollinationLevel : MonoBehaviour
{
    public int pollinationPercent, maxPollination = 100, pollenIncrease;
    public TextMeshProUGUI _pollinationLevel, _dOpt3;
    public GameObject levelCompleteUI, fader, pauseMenu;
    public bool levelCompleted, lvlCompLine, lineChanged;
    public AudioClip completeSFX;
    public string planet;
    private InputProfiler _controls;
    private AudioSource _audio;
    private bool _open;

    private void Awake()
    {
        _controls = new InputProfiler();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        pollinationPercent = PlayerPrefs.GetInt("PollinationLevel");
        if (!levelCompleted)
            pollinationPercent = 0;
        levelCompleteUI.SetActive(false);
    }

    private void Update()
    {
        PlayerPrefs.SetInt("PollinationLevel", pollinationPercent);
    }

    private void OnEnable()
    {
        _controls.InGameUI.LoadNextPlanet.started += LoadNextPlanet;
        _controls.InGameUI.OpenLevelCompleteUI.started += OpenLevelCompUI;
        _controls.InGameUI.LoadMainMenu.started += LoadMainMenu;
        _controls.InGameUI.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.LoadNextPlanet.started += LoadNextPlanet;
        _controls.InGameUI.OpenLevelCompleteUI.started += OpenLevelCompUI;
        _controls.InGameUI.LoadMainMenu.started -= LoadMainMenu;
        _controls.InGameUI.Disable();
    }

    private void OnGUI()
    {
        if (pollinationPercent <= maxPollination)
            _pollinationLevel.text = "POLLINATION: " + pollinationPercent + "%";

        if (pollinationPercent == maxPollination && !lvlCompLine)
        {
            _dOpt3.text = planet + " looks beautiful now!";
            lineChanged = true;
        }
    }

    public void IncreasePollination()
    {
        pollinationPercent += pollenIncrease;
        print("PollinationLevel: " + pollinationPercent);

        if (pollinationPercent == maxPollination)
        {
            // level complete
            levelCompleted = true;
            print("Level complete! " + levelCompleted);
            StartCoroutine(LevelCompleteUI());
        }
    }

    private void OpenLevelCompUI(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (levelCompleted && !_open)
            {
                _open = true;
                levelCompleteUI.SetActive(true);
            }
            else if (levelCompleted && _open)
            {
                _open = false;
                levelCompleteUI.SetActive(false);
            }
        }
    }

    private void LoadNextPlanet(InputAction.CallbackContext obj)
    {
        if (levelCompleted && levelCompleteUI.activeInHierarchy)
        {
            if (levelCompleteUI.activeInHierarchy)
            {
                print("Loading: " + planet);
                StartCoroutine(GoLoadLevel(planet));   
            }
        }
    }

    private void LoadMainMenu(InputAction.CallbackContext obj)
    {
        if (levelCompleteUI.activeInHierarchy)
        {
            print("Loading: MainMenu...");
            StartCoroutine(GoLoadLevel("001_MainMenu"));   
        }
    }
    
    private IEnumerator LevelCompleteUI()
    {
        yield return new WaitForSeconds(3f);
        levelCompleteUI.SetActive(true);
        _open = true;
        _audio.PlayOneShot(completeSFX);
    }

    private IEnumerator GoLoadLevel(string level)
    {
        levelCompleteUI.SetActive(false);
        print("Loading: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}