using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        foreach (var t in Gamepad.all)
            Debug.Log("Connected Gamepad: " + t.name);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}