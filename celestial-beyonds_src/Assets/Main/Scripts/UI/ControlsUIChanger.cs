using TMPro;
using UnityEngine;

public class ControlsUIChanger : MonoBehaviour
{
    public TextMeshProUGUI resumeBtn, mainMenuBtn, ctrlsBtn, photoMode, closePm, takePhoto, ctrlsBack;
    public TextMeshProUGUI opt1, opt2, opt3, talk, ask, quit, lightBtn;
    public TextMeshProUGUI c_openOrClose, nextPlanet;
    public TextMeshProUGUI[] a_opt1, a_opt2, a_opt3;
    public TextMeshProUGUI argylePrompt, viktorPrompt, upgradePrompt;

    public bool cin,
        photoModeInScene,
        moonbeam,
        stores,
        trappist,
        pcb,
        kepler,
        pollinationComplete,
        noArgyle,
        letThereBeLight;

    private int _peridotCost = 20;

    private void OnGUI()
    {
        if (Bools.keyboardSelected)
        {
            resumeBtn.text = "Resume: Ctrl";
            mainMenuBtn.text = "Main Menu: M";
        }

        if (Bools.playstationSelected)
        {
            // ●, ▲, ■
            resumeBtn.text = "Resume: Start";
            mainMenuBtn.text = "Main Menu: ●";
        }

        if (Bools.xboxSelected)
        {
            resumeBtn.text = "Resume: Start";
            mainMenuBtn.text = "Main Menu: B";
        }

        if (!cin)
        {
            if (Bools.keyboardSelected)
            {
                ctrlsBtn.text = "Controls: A";
                ctrlsBack.text = "Back: B";
            }

            if (Bools.playstationSelected)
            {
                ctrlsBtn.text = "Controls: ■";
                ctrlsBack.text = "Back: L1";
            }

            if (Bools.xboxSelected)
            {
                ctrlsBtn.text = "Controls: X";
                ctrlsBack.text = "Back: LB";
            }
        }

        if (photoModeInScene)
        {
            if (Bools.keyboardSelected)
            {
                photoMode.text = "C";
                closePm.text = "Back: C";
                takePhoto.text = "Take: ENTER";
            }

            if (Bools.playstationSelected)
            {
                photoMode.text = "R1";
                closePm.text = "Back: R1";
                takePhoto.text = "Take: R2";
            }

            if (Bools.xboxSelected)
            {
                photoMode.text = "RB";
                closePm.text = "Back: RB";
                takePhoto.text = "Take: RT";
            }
        }

        if (moonbeam)
        {
            if (Bools.keyboardSelected)
            {
                opt1.text = "1";
                opt2.text = "2";
                opt3.text = "3";
                for (var i = 0; i < 10; i++) a_opt1[i].text = "1";
                for (var i = 0; i < 10; i++) a_opt2[i].text = "2";
                for (var i = 0; i < 10; i++) a_opt3[i].text = "3";
                talk.text = "Talk to Moonbeam: T";
                ask.text = "Ask Moonbeam: I";
                quit.text = "quit: Q";
            }

            if (Bools.playstationSelected || Bools.xboxSelected)
            {
                opt1.text = "←";
                opt2.text = "↑";
                opt3.text = "→";
                for (var i = 0; i < 10; i++) a_opt1[i].text = "←";
                for (var i = 0; i < 10; i++) a_opt2[i].text = "↑";
                for (var i = 0; i < 10; i++) a_opt3[i].text = "→";
            }

            if (Bools.playstationSelected)
            {
                talk.text = "Talk to Moonbeam: L3";
                ask.text = "Ask Moonbeam: R3";
                quit.text = "quit: ●";
            }

            if (Bools.xboxSelected)
            {
                talk.text = "Talk to Moonbeam: LSB";
                ask.text = "Ask Moonbeam: RSB";
                quit.text = "quit: B";
            }
        }

        if (stores)
        {
            if (trappist)
                _peridotCost = 20;
            if (pcb)
                _peridotCost = 40;
            if (kepler)
                _peridotCost = 60;

            if (Bools.keyboardSelected)
            {
                if (!noArgyle)
                    argylePrompt.text = "Trade 1 Peridot for 10 Pollen? F";
                viktorPrompt.text = "Trade 1 Peridot for 10 Ammo? F";
                upgradePrompt.text = $"Trade {_peridotCost} Peridots for Upgrade? U";
            }

            if (Bools.playstationSelected)
            {
                if (!noArgyle)
                    argylePrompt.text = "Trade 1 Peridot for 10 Pollen? ▲";
                viktorPrompt.text = "Trade 1 Peridot for 10 Ammo? ▲";
            }

            if (Bools.xboxSelected)
            {
                if (!noArgyle)
                    argylePrompt.text = "Trade 1 Peridot for 10 Pollen? Y";
                viktorPrompt.text = "Trade 1 Peridot for 10 Ammo? Y";
            }

            if (Bools.playstationSelected || Bools.xboxSelected)
                upgradePrompt.text = $"Trade {_peridotCost} Peridots for Upgrade? ↑";
        }

        if (pollinationComplete)
        {
            if (Bools.keyboardSelected)
            {
                c_openOrClose.text = "Open/Close: O";
                nextPlanet.text = "Next Planet";
            }

            if (Bools.playstationSelected || Bools.xboxSelected)
            {
                c_openOrClose.text = "Open/Close: Select";
            }

            if (Bools.playstationSelected)
                nextPlanet.text = "Next Planet: ▲";

            if (Bools.xboxSelected)
                nextPlanet.text = "Next Planet: Y";
        }

        if (letThereBeLight)
        {
            if (Bools.keyboardSelected)
                lightBtn.text = "Activate Light: F";

            if (Bools.playstationSelected)
                lightBtn.text = "Activate Light: ▲";

            if (Bools.xboxSelected)
                lightBtn.text = "Activate Light: Y";
        }
    }
}