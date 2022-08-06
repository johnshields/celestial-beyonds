using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMainMenuUIChanger : MonoBehaviour
{
    public TextMeshProUGUI launchBtn, restartBtn, actBtn, ctrlsBtn, creditsBtn;
    public TextMeshProUGUI restartInput, closeActs, closeCtrls, closeCredits;
    public TextMeshProUGUI act1, act2, act3, act4, act5;

    private void Update()
    {
        if (Bools.playstationSelected || Bools.xboxSelected)
            launchBtn.text = "Launch: START";

        if (Bools.playstationSelected)
        {
            // Restart
            restartBtn.text = "Restart: ●";
            restartInput.text = "Yes: X     No: ●";
            // Controls
            ctrlsBtn.text = "Controls: ■";
            closeCtrls.text = "Close: ■";  
            // Credits
            creditsBtn.text = "Credits: R1";
            closeCredits.text = "End: R1";
            
            // ActLoader
            actBtn.text = "Load Act: ▲";
            closeActs.text = "Close: ▲";
            act1.text = "ACT ONE: X";
            act2.text = "ACT TWO: ●";
            act3.text = "ACT THREE: ■";
            act4.text = "ENDGAME: L1";
            act5.text = "EPILOGUE: R1";
        }
    }
}
