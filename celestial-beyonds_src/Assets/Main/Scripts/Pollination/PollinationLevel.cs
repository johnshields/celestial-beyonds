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
        
        // if all enemiesNum are dead
        if (miniMenu.GetComponent<MiniMenu>().enemiesNum == 10)
            exploreTxt.text = "Feel free to explore peacefully, Collect all Peridots and Find all the Artifacts!";
        // if all artifactsNum are found
        else if (miniMenu.GetComponent<MiniMenu>().artifactsNum == 10)
            exploreTxt.text = "Feel free to explore more, Collect all Peridots and Terminate all Enemies!";
        // if all peridotsNum are collect
        else if (miniMenu.GetComponent<MiniMenu>().peridotsNum == 66)
            exploreTxt.text = "Feel free to explore more, Terminate all Enemies and Find all the Artifacts!";
        // if all enemiesNum are dead and all artifactsNum are found
        else if (miniMenu.GetComponent<MiniMenu>().enemiesNum == 10 && miniMenu.GetComponent<MiniMenu>().artifactsNum == 10)
            exploreTxt.text = "Feel free to explore peacefully and Collect all Peridots!";
        else if (miniMenu.GetComponent<MiniMenu>().peridotsNum == 66 && miniMenu.GetComponent<MiniMenu>().enemiesNum == 10)
            exploreTxt.text = "Feel free to explore peacefully and Find all the Artifacts!";
        // if all peridotsNum are collect and all artifactsNum are found
        else if (miniMenu.GetComponent<MiniMenu>().peridotsNum == 66 && miniMenu.GetComponent<MiniMenu>().artifactsNum == 10)
            exploreTxt.text = "Feel free to explore more, Terminate all Enemies and Find all the Artifacts!";
        // if all everything is done
        else if (miniMenu.GetComponent<MiniMenu>().enemiesNum == 10 && miniMenu.GetComponent<MiniMenu>().artifactsNum == 10 && 
                 miniMenu.GetComponent<MiniMenu>().peridotsNum == 66)
            exploreTxt.text = "Feel free to explore peacefully!";
        // original
        else if (miniMenu.GetComponent<MiniMenu>().enemiesNum != 10 && miniMenu.GetComponent<MiniMenu>().artifactsNum != 10 && 
                 miniMenu.GetComponent<MiniMenu>().peridotsNum != 66)
            exploreTxt.text =
                "Feel free to explore more, Collect all Peridots, Terminate all Enemies and Find all the Artifacts!";
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