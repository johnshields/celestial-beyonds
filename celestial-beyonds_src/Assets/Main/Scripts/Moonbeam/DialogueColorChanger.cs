using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueColorChanger : MonoBehaviour
{
    public GameObject[] dialogueOpt, artifactOpt;
    private readonly Color32 _green = new Color32(20, 255, 0, 225);
    private readonly Color32 _white = new Color32(255, 255, 255, 225);

    public void ChangeTextColor(int whichOne)
    {
        // General
        switch (whichOne)
        {
            case 100:
                print("change" + whichOne);
                DialogueUIHighlight(dialogueOpt, 0, 1, 2, _green, _white, _white);
                StartCoroutine(ResetGeneralDColor());
                break;
            case 101:
                print("change" + whichOne);
                DialogueUIHighlight(dialogueOpt, 0, 1, 2, _white, _green, _white);
                StartCoroutine(ResetGeneralDColor());
                break;
            case 102:
                print("change" + whichOne);
                DialogueUIHighlight(dialogueOpt, 0, 1, 2, _white, _white, _green);
                StartCoroutine(ResetGeneralDColor());
                break;
        }

        // Artifacts
        switch (whichOne)
        {
            // Artifacts 0
            case 0:
                DialogueUIHighlight(artifactOpt, 0, 1, 2, _green, _white, _white);
                break;
            case 1:
                DialogueUIHighlight(artifactOpt, 0, 1, 2, _white, _green, _white);
                break;
            case 2:
                DialogueUIHighlight(artifactOpt, 0, 1, 2, _white, _white, _green);
                break;
            // Artifact 1
            case 3:
                DialogueUIHighlight(artifactOpt, 3, 4, 5, _green, _white, _white);
                break;
            case 4:
                DialogueUIHighlight(artifactOpt, 3, 4, 5, _white, _green, _white);
                break;
            case 5:
                DialogueUIHighlight(artifactOpt, 3, 4, 5, _white, _white, _green);
                break;
            // Artifact 2
            case 6:
                DialogueUIHighlight(artifactOpt, 6, 7, 8, _green, _white, _white);
                break;
            case 7:
                DialogueUIHighlight(artifactOpt, 6, 7, 8, _white, _green, _white);
                break;
            case 8:
                DialogueUIHighlight(artifactOpt, 6, 7, 8, _white, _white, _green);
                break;
            // Artifact 3
            case 9:
                DialogueUIHighlight(artifactOpt, 9, 10, 11, _green, _white, _white);
                break;
            case 10:
                DialogueUIHighlight(artifactOpt, 9, 10, 11, _white, _green, _white);
                break;
            case 11:
                DialogueUIHighlight(artifactOpt, 9, 10, 11, _white, _white, _green);
                break;
            // Artifact 4
            case 12:
                DialogueUIHighlight(artifactOpt, 12, 13, 14, _green, _white, _white);
                break;
            case 13:
                DialogueUIHighlight(artifactOpt, 12, 13, 14, _white, _green, _white);
                break;
            case 14:
                DialogueUIHighlight(artifactOpt, 12, 13, 14, _white, _white, _green);
                break;
            // Artifact 5
            case 15:
                DialogueUIHighlight(artifactOpt, 15, 16, 17, _green, _white, _white);
                break;
            case 16:
                DialogueUIHighlight(artifactOpt, 15, 16, 17, _white, _green, _white);
                break;
            case 17:
                DialogueUIHighlight(artifactOpt, 15, 16, 17, _white, _white, _green);
                break;
            // Artifact 6
            case 18:
                DialogueUIHighlight(artifactOpt, 18, 19, 20, _green, _white, _white);
                break;
            case 19:
                DialogueUIHighlight(artifactOpt, 18, 19, 20, _white, _green, _white);
                break;
            case 20:
                DialogueUIHighlight(artifactOpt, 18, 19, 20, _white, _white, _green);
                break;
            // Artifact 7
            case 21:
                DialogueUIHighlight(artifactOpt, 21, 22, 23, _green, _white, _white);
                break;
            case 22:
                DialogueUIHighlight(artifactOpt, 21, 22, 23, _white, _green, _white);
                break;
            case 23:
                DialogueUIHighlight(artifactOpt, 21, 22, 23, _white, _white, _green);
                break;
            // Artifact 8
            case 24:
                DialogueUIHighlight(artifactOpt, 24, 25, 26, _green, _white, _white);
                break;
            case 25:
                DialogueUIHighlight(artifactOpt, 24, 25, 26, _white, _green, _white);
                break;
            case 26:
                DialogueUIHighlight(artifactOpt, 24, 25, 26, _white, _white, _green);
                break;
            // Artifact 9
            case 27:
                DialogueUIHighlight(artifactOpt, 27, 28, 29, _green, _white, _white);
                break;
            case 28:
                DialogueUIHighlight(artifactOpt, 27, 28, 29, _white, _green, _white);
                break;
            case 29:
                DialogueUIHighlight(artifactOpt, 27, 28, 29, _white, _white, _green);
                break;
        }
    }

    private void DialogueUIHighlight(GameObject[] opt, int o0, int o1, int o2, Color32 c1, Color32 c2, Color32 c3)
    {
        opt[o0].GetComponent<TextMeshProUGUI>().color = c1;
        opt[o1].GetComponent<TextMeshProUGUI>().color = c2;
        opt[o2].GetComponent<TextMeshProUGUI>().color = c3;
    }
    

    private IEnumerator ResetGeneralDColor()
    {
        yield return new WaitForSeconds(1f);
        DialogueUIHighlight(dialogueOpt, 0, 1, 2, _white, _white, _white);
    }
}