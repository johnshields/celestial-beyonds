using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Main.Scripts.Enemies.Aristaues
{
    // Aristaues battle cheat: softens the boss when the fight is too hard.
    public class Lemons : MonoBehaviour
    {
        private static bool l, e, m, o, n;
        public static bool cheatActivated;
        private InputProfiler _controls;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void HookSceneLoad()
        {
            SceneManager.sceneLoaded += (_, __) => ResetForNewLevel();
        }

        // Per-level: cheat must be retyped on every Aristaues attempt.
        private static void ResetForNewLevel()
        {
            cheatActivated = false;
            l = e = m = o = n = false;
        }

        private void Awake()
        {
            _controls = new InputProfiler();
        }

        private void OnEnable()
        {
            _controls.Lemons.LetterL.started += LetterL;
            _controls.Lemons.LetterE.started += LetterE;
            _controls.Lemons.LetterM.started += LetterM;
            _controls.Lemons.LetterO.started += LetterO;
            _controls.Lemons.LetterN.started += LetterN;
            _controls.Lemons.LetterS.started += LetterS;
            _controls.Lemons.Enable();
        }

        private void OnDisable()
        {
            _controls.Lemons.LetterL.started -= LetterL;
            _controls.Lemons.LetterE.started -= LetterE;
            _controls.Lemons.LetterM.started -= LetterM;
            _controls.Lemons.LetterO.started -= LetterO;
            _controls.Lemons.LetterN.started -= LetterN;
            _controls.Lemons.LetterS.started -= LetterS;
            _controls.Lemons.Disable();
        }

        private void LetterL(InputAction.CallbackContext obj)
        {
            l = true;
        }

        private void LetterE(InputAction.CallbackContext obj)
        {
            if (l) e = true;
        }

        private void LetterM(InputAction.CallbackContext obj)
        {
            if (l && e) m = true;
        }

        private void LetterO(InputAction.CallbackContext obj)
        {
            if (l && e && m) o = true;
        }

        private void LetterN(InputAction.CallbackContext obj)
        {
            if (l && e && m && o) n = true;
        }

        private void LetterS(InputAction.CallbackContext obj)
        {
            if (l && e && m && o && n)
                Activate();
        }

        private void Activate()
        {
            if (cheatActivated) return;
            cheatActivated = true;
            l = e = m = o = n = false;
            var boss = GetComponent<AristauesProfiler>();
            if (boss != null) boss.aristauesHealth = 100;
            print($"Cheat Lemons activated: {cheatActivated}");
        }
    }
}
