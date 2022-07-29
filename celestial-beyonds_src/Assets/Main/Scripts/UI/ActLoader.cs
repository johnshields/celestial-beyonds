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
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.LoadActOne.started -= LoadActOne;
        _controls.UIActions.LoadActTwo.started -= LoadActTwo;
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

    private IEnumerator LaunchAct(string level)
    {
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}