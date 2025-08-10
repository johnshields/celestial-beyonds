using UnityEngine;
using UnityEngine.UI;

public class ScarlettPhotographerBot : MonoBehaviour
{
    public Text status, photoIDTxt;
    private bool _success, _trySend;
    private long _code;

    private void OnGUI()
    {
        if (_trySend)
        {
            photoIDTxt.text = "Datetime stamp: " + GetComponent<PhotoMode>().photoID + " -> Copied to clipboard!";
            status.text = "Success: " + _success + " -> " + _code;
        }
    }
}