using Main.Scripts.UI.CursorControls;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject keyboard, playstation, xbox, ctrlsPanel, planetPanel;
    public bool isMainMenu;
    private GameObject _cursor;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void Update()
    {
        if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "Keyboard")
            KeyboardControls();
        else if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "PlayStation")
            PlayStationControls();
        else if (_cursor.GetComponent<CursorClickedOn>().ReturnClickedElement() == "Xbox")
            XboxControls();
    }

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
    }
}
