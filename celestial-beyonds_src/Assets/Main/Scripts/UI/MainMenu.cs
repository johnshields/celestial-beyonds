using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _fader;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        _fader.GetComponent<Animator>().SetBool("FadeIn", true);
        _fader.GetComponent<Animator>().SetBool("FadeOut", false);
    }

    private void OnEnable()
    {
        _controls.UIActions.StartGame.started += StartGame;
        _controls.UIActions.LoadTBOne.started += LoadTBOne;
        _controls.UIActions.LoadTBTwo.started += LoadTBTwo;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.LoadTBOne.started -= LoadTBOne;
        _controls.UIActions.LoadTBTwo.started -= LoadTBTwo;
        _controls.UIActions.Disable();
    }

    private void LoadTBOne(InputAction.CallbackContext obj)
    {
        print("Loading into: TestBox001");
        StartCoroutine(LaunchGame(4));
    }

    private void LoadTBTwo(InputAction.CallbackContext obj)
    {
        print("Loading into: MoonBeamBox");
        StartCoroutine(LaunchGame(6));
    }

    private void StartGame(InputAction.CallbackContext obj)
    {
        print("Loading into: 002_Opening");
        StartCoroutine(LaunchGame(1));
    }


    private IEnumerator LaunchGame(int level)
    {
        print("Loading into: " + level);
        _fader.GetComponent<Animator>().SetBool("FadeIn", false);
        _fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}