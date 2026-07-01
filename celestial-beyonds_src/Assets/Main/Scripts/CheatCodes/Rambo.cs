using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Rambo : MonoBehaviour
{
    private static bool r, a, m, b, o;
    public static bool cheatActivated;
    private InputProfiler _controls;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void HookSceneLoad()
    {
        SceneManager.sceneLoaded += (_, __) => ResetForNewLevel();
    }

    // Per-level: wipe cheat state and any Bools leakage, then rebase to persisted upgrades.
    private static void ResetForNewLevel()
    {
        cheatActivated = false;
        r = a = m = b = o = false;
        Bools.cdUpgraded = PlayerMemory.cannonUpgrade == 2;
        Bools.aUpgraded = PlayerMemory.armorUpgrade == 1;
    }

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
        _controls.Rambo.Disable();
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
        if (r && a && m && b)
        {
            o = true;
            Activate();
        }
    }

    // Defier and Armor granted in-memory only; no PlayerPrefs write, wiped on next scene load.
    private void Activate()
    {
        if (cheatActivated) return;
        cheatActivated = true;
        r = a = m = b = o = false;
        Bools.cdUpgraded = true;
        Bools.aUpgraded = true;
        var cas = GetComponent<CaptainAnimAndSound>();
        if (cas != null)
        {
            cas.cdUpgrade = true;
            cas.aUpgrade = true;
        }
        print($"Cheat Rambo activated: {cheatActivated}");
    }
}
