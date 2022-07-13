using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PollinationLevel : MonoBehaviour
{
    public int pollinationPercent, maxPollination = 100, pollenIncrease;
    public TextMeshProUGUI _pollinationLevel, exploreTxt;
    public GameObject levelCompleteUI, fader, pauseMenu, miniMenu;
    public bool levelCompleted;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
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
        _controls.InGameUI.CloseLevelCompleteUI.started += CloseLevelCompUI;
        _controls.InGameUI.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.LoadNextPlanet.started += LoadNextPlanet;
        _controls.InGameUI.OpenLevelCompleteUI.started += OpenLevelCompUI;
        _controls.InGameUI.CloseLevelCompleteUI.started += CloseLevelCompUI;
        _controls.InGameUI.Disable();
    }

    private void OnGUI()
    {
        if (pollinationPercent <= maxPollination)
            _pollinationLevel.text = "POLLINATION: " + pollinationPercent + "%";
        
        if (miniMenu.GetComponent<MiniMenu>().enemiesNum == 10)
            exploreTxt.text = "Feel free to explore more, Collect more Peridots or Find all the Artifacts!";
        else if (miniMenu.GetComponent<MiniMenu>().artifactsNum == 10)
            exploreTxt.text = "Feel free to explore more, Collect more Peridots or Terminate more Enemies!";
        else if (miniMenu.GetComponent<MiniMenu>().enemiesNum == 10 && miniMenu.GetComponent<MiniMenu>().artifactsNum == 10)
            exploreTxt.text = "Feel free to explore more and Collect more Peridots!";
        else exploreTxt.text =
                "Feel free to explore more, Collect more Peridots, Terminate more Enemies or Find all the Artifacts!";
    }

    public void IncreasePollination()
    {
        pollinationPercent += pollenIncrease;

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
            if (levelCompleted)
                levelCompleteUI.SetActive(true);
        }
    }

    private void CloseLevelCompUI(InputAction.CallbackContext obj)
    {
        if (!pauseMenu.GetComponent<InGameMenus>().pausedActive)
        {
            if (levelCompleted)
                levelCompleteUI.SetActive(false);
        }
    }

    private void LoadNextPlanet(InputAction.CallbackContext obj)
    {
        if (levelCompleted && levelCompleteUI.activeInHierarchy)
        {
            print("Loading: " + SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LevelCompleteUI()
    {
        yield return new WaitForSeconds(3f);
        levelCompleteUI.SetActive(true);
    }

    private IEnumerator LoadNextScene()
    {
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}