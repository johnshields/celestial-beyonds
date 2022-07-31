using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightControl : MonoBehaviour
{
    public GameObject puzzle, lightBtn;
    public bool lightActivated, dungeonOpen;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }
    
    private void OnEnable()
    {
        _controls.Profiler.OpenDungeon.started += OpenDungeonCtrl;
        _controls.Profiler.Enable();
    }

    private void OnDisable()
    {
        _controls.Profiler.OpenDungeon.started -= OpenDungeonCtrl;
        _controls.Profiler.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dungeonOpen)
        {
            lightActivated = true;   
            lightBtn.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            lightBtn.SetActive(false);
    }

    private void OpenDungeonCtrl(InputAction.CallbackContext obj)
    {
        if (lightActivated && !dungeonOpen)
        {
            dungeonOpen = true;
            puzzle.GetComponent<DungeonPuzzle>().ActivateLight();
            StartCoroutine(OpenDungeon());
        }
    }

    private IEnumerator OpenDungeon()
    {
        yield return new WaitForSeconds(1);
        lightBtn.SetActive(false);
        yield return new WaitForSeconds(2);
        puzzle.GetComponent<DungeonPuzzle>().OpenDungeon();
        print("Dungeon Open!");
    }
}
