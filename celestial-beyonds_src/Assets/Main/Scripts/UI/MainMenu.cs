using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject fader;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void Start()
    {
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
    }

    private void OnEnable()
    {
        _controls.UIActions.StartGame.started += StartGame;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.StartGame.started -= StartGame;
        _controls.UIActions.Disable();
    }

    // ControllerInput
    private void StartGame(InputAction.CallbackContext obj)
    {
        StartCoroutine(LaunchGame(1));
    }

    // MouseUI Input
    public void StartGameM()
    {
        StartCoroutine(LaunchGame(1));
    }


    private IEnumerator LaunchGame(int level)
    {
        fader.SetActive(true);
        print("Loading into: " + level);
        fader.GetComponent<Animator>().SetBool("FadeIn", false);
        fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(level);
    }
}