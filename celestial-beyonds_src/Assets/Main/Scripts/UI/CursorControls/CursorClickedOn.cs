using UnityEngine;

namespace Main.Scripts.UI.CursorControls
{
    public class CursorClickedOn : MonoBehaviour
    {
        private GameObject _cursor;
        private string returnVal;

        private void Awake()
        {
            _cursor = GameObject.FindGameObjectWithTag("Cursor");
        }

        public string ReturnClickedElement()
        {
            // MainMenu Buttons
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "LaunchBtn")
                returnVal = "LaunchBtn";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Restart")
                returnVal = "Restart";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "LoadAct")
                returnVal = "LoadAct";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Controls")
                returnVal = "Controls";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Credits")
                returnVal = "Credits";
            
            // Sub Buttons - Close
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseActs")
                returnVal = "CloseActs";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseControls")
                returnVal = "CloseControls";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "CloseCredits")
                returnVal = "CloseCredits";
            
            // ConfirmBox (Restart)
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ConfirmYes")
                returnVal = "ConfirmYes";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ConfirmNo")
                returnVal = "ConfirmNo";
            
            // Acts
            if (_cursor.GetComponent<ControllerCursor>().clickedElement =="ActOne")
                returnVal = "ActOne";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ActTwo")
                returnVal = "ActTwo";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "ActThree")
                returnVal = "ActThree";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement =="Endgame")
                returnVal = "Endgame";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement =="Epilogue")
                returnVal = "Epilogue";
            
            // Different InputDevices
            if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Keyboard")
                returnVal = "Keyboard";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "PS")
                returnVal = "PlayStation";
            else if (_cursor.GetComponent<ControllerCursor>().clickedElement == "Xbox")
                returnVal = "Xbox";

            return returnVal;
        }
    }
}