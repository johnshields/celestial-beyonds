using Main.Scripts.UI.CursorControls;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenu : MonoBehaviour
{
    public GameObject keyboard, playstation, xbox, ctrlsPanel, planetPanel;
    public bool isMainMenu;
    public bool noCursor;
    private GameObject _cursor;
    private InputProfiler _controls;


    private void Awake()
    {
        _controls = new InputProfiler();
        if (!noCursor)
            _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void Update()
    {
        if (!noCursor)
        {
            if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "Keyboard")
                KeyboardControls();
            else if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "PlayStation")
                PlayStationControls();
            else if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "Xbox")
                XboxControls();
        }
    }

    private void OnEnable()
    {
        _controls.InGameUI.Keyboard.started += KeyboardCtrls;
        _controls.InGameUI.PlayStation.started += PlayStationCtrls;
        _controls.InGameUI.Xbox.started += XboxCtrls;
        _controls.InGameUI.Enable();
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameUI.Keyboard.started += KeyboardCtrls;
        _controls.InGameUI.PlayStation.started += PlayStationCtrls;
        _controls.InGameUI.Xbox.started += XboxCtrls;
        _controls.InGameUI.Disable();
        _controls.UIActions.Disable();
    }

    // Controller
    private void KeyboardCtrls(InputAction.CallbackContext obj)
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Keyboard");
        WhichControls(true, false, false);
    }

    private void PlayStationCtrls(InputAction.CallbackContext obj)
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Playstation Controller");
        WhichControls(false, true, false);
    }

    private void XboxCtrls(InputAction.CallbackContext obj)
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Xbox Controller");
        WhichControls(false, false, true);
    }

    // Cursor
    private void KeyboardControls()
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Keyboard");
        WhichControls(true, false, false);
    }

    private void PlayStationControls()
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Playstation Controller");
        WhichControls(false, true, false);
    }

    private void XboxControls()
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Xbox Controller");
        WhichControls(false, false, true);
    }

    private void WhichControls(bool k, bool p, bool x)
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        Bools.keyboardSelected = k;
        Bools.playstationSelected = p;
        Bools.xboxSelected = x;
        if (isMainMenu)
        {
            if (planetPanel.activeInHierarchy) return;
            keyboard.SetActive(k);
            playstation.SetActive(p);
            xbox.SetActive(x);
        }
        else
        {
            keyboard.SetActive(k);
            playstation.SetActive(p);
            xbox.SetActive(x);
        }
    }
}