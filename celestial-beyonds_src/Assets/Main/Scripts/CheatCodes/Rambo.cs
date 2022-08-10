using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rambo : MonoBehaviour
{
    private static bool r, a, m, b, o;
    public static bool cheatActivated;
    private InputProfiler _controls;

    private void Awake()
    {
        _controls = new InputProfiler();
    }

    private void OnEnable()
    {
        _controls.Rambo.LetterR.started += LetterR;
        _controls.Rambo.LetterA.started += LetterA;
        _controls.Rambo.LetterM.started += LetterM;
        _controls.Rambo.LetterB.started += LetterB;
        _controls.Rambo.LetterO.started += LetterO;
        _controls.Rambo.Enable();
    }

    private void OnDisable()
    {
        _controls.Rambo.LetterR.started -= LetterR;
        _controls.Rambo.LetterA.started -= LetterA;
        _controls.Rambo.LetterM.started -= LetterM;
        _controls.Rambo.LetterB.started -= LetterB;
        _controls.Rambo.LetterO.started -= LetterO;
        _controls.Lemons.Disable();
    }

    private void Update()
    {
        ActivateRambo();
    }

    private void LetterR(InputAction.CallbackContext obj)
    {
        r = true;
    }

    private void LetterA(InputAction.CallbackContext obj)
    {
        if (r) a = true;
    }

    private void LetterM(InputAction.CallbackContext obj)
    {
        if (r && a) m = true;
    }

    private void LetterB(InputAction.CallbackContext obj)
    {
        if (r && a && m) b = true;
    }

    private void LetterO(InputAction.CallbackContext obj)
    {
        if (r && a && m && b) o = true;
    }


    private void ActivateRambo()
    {
        if (r && a && m && b && o && !cheatActivated)
        {
            cheatActivated = true;
            // Defier and Armor
            Bools.cdUpgraded = true;
            GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
            PlayerMemory.cannonUpgrade = 2;
            Bools.aUpgraded = true;
            GetComponent<CaptainAnimAndSound>().aUpgrade = true;
            print($"Cheat Rambo activated: {cheatActivated}");
        }
    }
}