using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _fader;
    private UIActionsProfiler _controls;

    private void Awake()
    {
        _controls = new UIActionsProfiler();
    }

    private void Start()
    {
        _fader.GetComponent<Animator>().SetBool("FadeIn", true);
        _fader.GetComponent<Animator>().SetBool("FadeOut", false);
    }

    private void OnEnable()
    {
        _controls.UIActions.StartGame.started += StartGame;
        _controls.UIActions.LoadTestBoxOne.started += LoadTestBox1;
        _controls.UIActions.LoadTestBoxTwo.started += LoadTestBox2;
        _controls.UIActions.LoadTestBoxThree.started += LoadTestBox3;
        _controls.UIActions.StartGame.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.LoadTestBoxOne.started -= LoadTestBox1;
        _controls.UIActions.LoadTestBoxTwo.started -= LoadTestBox2;
        _controls.UIActions.LoadTestBoxThree.started -= LoadTestBox3;
        _controls.UIActions.StartGame.Disable();
    }

    private void StartGame(InputAction.CallbackContext obj)
    {
        print("Loading into: 002_Opening");
        StartCoroutine(LaunchGame("002_Opening"));
    }
    
    private void LoadTestBox1(InputAction.CallbackContext obj)
    {
        print("Loading into: TestBox");
        SceneManager.LoadScene("TestBox");
    }
    
    private void LoadTestBox2(InputAction.CallbackContext obj)
    {
        print("Loading into: MoonbeamTestBox");
        SceneManager.LoadScene("MoonbeamTestBox");
    }
    
    private void LoadTestBox3(InputAction.CallbackContext obj)
    {
        print("Loading into: ...");
        print("no action yet.");
    }

    private IEnumerator LaunchGame(string level)
    {
        print("Loading into: " + level);
        _fader.GetComponent<Animator>().SetBool("FadeIn", false);
        _fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}