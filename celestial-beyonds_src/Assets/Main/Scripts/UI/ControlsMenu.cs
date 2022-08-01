using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenu : MonoBehaviour
{
    public GameObject keyboard, playstation, xbox, ctrlsPanel, planetPanel;
    public TextMeshProUGUI[] keyboardTxt, playstationText, xboxTxt;
    public bool isMainMenu;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }
    
    private void OnEnable()
    {
        _controls.UIActions.KeyboardControls.started += KeyboardControls;
        _controls.UIActions.PlayStationControls.started += PlayStationControls;
        _controls.UIActions.XboxControls.started += XboxControls;
        _controls.UIActions.Enable();
    }

    private void OnDisable()
    {
        _controls.UIActions.KeyboardControls.started -= KeyboardControls;
        _controls.UIActions.PlayStationControls.started -= PlayStationControls;
        _controls.UIActions.XboxControls.started -= XboxControls;
        _controls.UIActions.Disable();
    }

    private void KeyboardControls(InputAction.CallbackContext obj)
    {
        print("Keyboard");
        WhichControls(true, false, false);
    }

    private void PlayStationControls(InputAction.CallbackContext obj)
    {
        print("Playstation Controller");
        WhichControls(false, true, false);
    }

    private void XboxControls(InputAction.CallbackContext obj)
    {
        print("Xbox Controller");
        WhichControls(false, false, true);
    }

    private void WhichControls(bool k, bool p, bool x)
    {
        if (!ctrlsPanel.activeInHierarchy) return;
        if (!planetPanel.activeInHierarchy && isMainMenu)
        {
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

        //Booleans.keyboardSelected = k;
        //Booleans.playstationSelected = p;
        //Booleans.xboxSelected = x;
    }

    private void OnGUI()
    {
        if (Bools.keyboardSelected)
        {
            keyboardTxt[0].text = "Keyboard: K";
            playstationText[0].text = "Playstation: P";
            xboxTxt[0].text = "Xbox: X";
        }
        else if (Bools.playstationSelected)
        {
            keyboardTxt[0].text = "Keyboard: ■";
            playstationText[0].text = "Playstation: ▲";
            xboxTxt[0].text = "Xbox: X";
        }
        else if (Bools.xboxSelected)
        {
            keyboardTxt[0].text = "Keyboard: X";
            playstationText[0].text = "Playstation: Y";
            xboxTxt[0].text = "Xbox: A";
        }
    }
}
