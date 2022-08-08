using System.Collections;
using Main.Scripts.UI.CursorControls;
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
    public string nextPlanet;
    private InputProfiler _controls;
    private AudioSource _audio;
    private bool _open;
    private GameObject _cursor;

    private void Awake()
    {
        _controls = new InputProfiler();
        _audio = GetComponent<AudioSource>();
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
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
        
        if (_cursor.GetComponent<ControllerCursor>().clickedElement == "NextPlanetComp")
            StartCoroutine(GoLoadLevel(nextPlanet));
        else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "MainMenuComp")
            StartCoroutine(GoLoadLevel("101_MainMenu"));
    }

    private void OnEnable()
    {
        _controls.InGameUI.LoadNextPlanet.started += LoadNextPlanet;
        _controls.InGameUI.OpenLevelCompleteUI.started += OpenLevelCompUI;
        _controls.InGameUI.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.LoadNextPlanet.started += LoadNextPlanet;
        _controls.InGameUI.OpenLevelCompleteUI.started += OpenLevelCompUI;
        _controls.InGameUI.Disable();
    }

    private void OnGUI()
    {
        if (pollinationPercent <= maxPollination)
            _pollinationLevel.text = "POLLINATION: " + pollinationPercent + "%";

        if (pollinationPercent == maxPollination && !lvlCompLine)
        {
            _dOpt3.text = nextPlanet + " looks beautiful now!";
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
                GameObject.Find("ControllerCursor/Controller/Cursor").SetActive(true);
                _cursor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                Bools.cursorRequired = true;
                Cursor.visible = true;
                _open = true;
                levelCompleteUI.SetActive(true);
            }
            else if (levelCompleted && _open)
            {
                GameObject.Find("ControllerCursor/Controller/Cursor").SetActive(false);
                _cursor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                Bools.cursorRequired = false;
                Cursor.visible = false;
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
                print("Loading: " + nextPlanet);
                StartCoroutine(GoLoadLevel(nextPlanet));   
            }
        }
    }

    private IEnumerator LevelCompleteUI()
    {
        yield return new WaitForSeconds(3f);
        GameObject.Find("ControllerCursor/Controller/Cursor").SetActive(true);
        Cursor.visible = true;
        Bools.cursorRequired = true;
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