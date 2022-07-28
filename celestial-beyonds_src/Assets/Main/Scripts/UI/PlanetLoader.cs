using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlanetLoader : MonoBehaviour
{
    public GameObject planetPanel, fader;
    private InputProfiler _controls;
    
    private void Awake()
    {
        _controls = new InputProfiler();
    }
    
    private void OnEnable()
    {
        _controls.UIActions.LoadTrappist.started += LoadTrappist;
        _controls.UIActions.LoadPCB.started += LoadPCB;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.LoadTrappist.started -= LoadTrappist;
        _controls.UIActions.LoadPCB.started -= LoadPCB;
        _controls.UIActions.Disable();
    }

    private void LoadTrappist(InputAction.CallbackContext obj)
    {
        if (planetPanel.activeInHierarchy)
        {
            StartCoroutine(LaunchPlanet("004_Intro_TRAPPIST-1"));
        }
    }

    private void LoadPCB(InputAction.CallbackContext obj)
    {
        if (planetPanel.activeInHierarchy)
        {
            StartCoroutine(LaunchPlanet("007_ProximaCentauriB"));
        }
    }
    
    private IEnumerator LaunchPlanet(string level)
    {
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool($"FadeIn", false);
        fader.GetComponent<Animator>().SetBool($"FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}
