using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.Enemies.Aristaues
{
    // Script for Aristaues Battle Cheat (if players find it too hard)
    public class Lemons : MonoBehaviour
    {
        private static bool l, e, m, o, n, s;
        public bool cheatActivated;
        private InputProfiler _controls;

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

        private void Update()
        {
            ActivateLemons();
        }

        private void LetterL(InputAction.CallbackContext obj)
        {
            l = true;
        }

        private void LetterE(InputAction.CallbackContext obj)
        {
            if(l) e = true;
        }

        private void LetterM(InputAction.CallbackContext obj)
        {
            if(l && e) m = true;
        }

        private void LetterO(InputAction.CallbackContext obj)
        {
            if(l && e && m) o = true;
        }

        private void LetterN(InputAction.CallbackContext obj)
        {
            if(l && e && m && o) n = true;
        }

        private void LetterS(InputAction.CallbackContext obj)
        {
            if(l && e && m && o && n) s = true;
        }

        private void ActivateLemons()
        {
            if (l && e && m && o && n && s && !cheatActivated)
            {
                cheatActivated = true;
                // Weaken Aristaues
                GetComponent<AristauesProfiler>().aristauesHealth = 100;
                print($"Cheat Lemons activated: {cheatActivated}");
            }
        }    
    }
}