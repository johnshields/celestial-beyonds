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
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Keyboard")
                KeyboardControls();
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "PS")
                PlayStationControls();
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Xbox")
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
    public void KeyboardControls()
    {
        ClickedElementEmpty();
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Keyboard");
        WhichControls(true, false, false);
    }

    public void PlayStationControls()
    {
        ClickedElementEmpty();
        if (!ctrlsPanel.activeInHierarchy) return;
        print("Playstation Controller");
        WhichControls(false, true, false);
    }

    public void XboxControls()
    {
        ClickedElementEmpty();
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
    
    private void ClickedElementEmpty()
    {
        _cursor.GetComponent<ControllerCursor>().clickedElement = string.Empty;
    }
}