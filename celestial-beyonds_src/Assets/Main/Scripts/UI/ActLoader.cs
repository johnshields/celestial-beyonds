using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ActLoader : MonoBehaviour
{
    public GameObject actPanel, fader;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void OnEnable()
    {
        _controls.UIActions.LoadActOne.started += LoadActOne;
        _controls.UIActions.LoadActTwo.started += LoadActTwo;
        _controls.UIActions.LoadActThree.started += LoadActThree;
        _controls.UIActions.LoadEndgame.started += LoadEndgame;
        _controls.UIActions.LoadEpilogue.started += LoadEpilogue;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.LoadActOne.started -= LoadActOne;
        _controls.UIActions.LoadActTwo.started -= LoadActTwo;
        _controls.UIActions.LoadActThree.started -= LoadActThree;
        _controls.UIActions.LoadEndgame.started -= LoadEndgame;
        _controls.UIActions.LoadEpilogue.started -= LoadEpilogue;
        _controls.UIActions.Disable();
    }

    private void LoadActOne(InputAction.CallbackContext obj)
    {
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("003_CelestialWaltz"));
    }

    private void LoadActTwo(InputAction.CallbackContext obj)
    {
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("005_LunarPulse"));
    }
    
    private void LoadActThree(InputAction.CallbackContext obj)
    {
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("007_Man_of_Celestial_Man_of_Faith"));
    }
    
    private void LoadEndgame(InputAction.CallbackContext obj)
    {
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("009_Intro_Endgame"));
    }
    
    private void LoadEpilogue(InputAction.CallbackContext obj)
    {
        if (actPanel.activeInHierarchy)
            StartCoroutine(LaunchAct("010_TheGoldenRecord"));
    }

    private IEnumerator LaunchAct(string level)
    {
        actPanel.SetActive(false);
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}