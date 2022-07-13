using TMPro;
using UnityEngine;

public class DialogueForm : MonoBehaviour
{
    private GameObject _mb;

    private void Awake()
    {
        _mb = GameObject.FindGameObjectWithTag("Moonbeam");
    }

    public void SetUpForm(WWWForm form)
    {
        switch (_mb.GetComponent<MoonbeamDialogue>().whichDialogue)
        {
            // general
            case 100:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[0].GetComponent<TextMeshProUGUI>().text);
                break;
            case 101:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[1].GetComponent<TextMeshProUGUI>().text);
                break;
            case 102:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().dialogueOptions[2].GetComponent<TextMeshProUGUI>().text);
                break;
            // artifacts
            case 0:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[0].GetComponent<TextMeshProUGUI>().text);
                break;
            case 1:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[1].GetComponent<TextMeshProUGUI>().text);
                break;
            case 2:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[2].GetComponent<TextMeshProUGUI>().text);
                break;
            case 3:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[3].GetComponent<TextMeshProUGUI>().text);
                break;
            case 4:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[4].GetComponent<TextMeshProUGUI>().text);
                break;
            case 5:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[5].GetComponent<TextMeshProUGUI>().text);
                break;
            case 6:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[6].GetComponent<TextMeshProUGUI>().text);
                break;
            case 7:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[7].GetComponent<TextMeshProUGUI>().text);
                break;
            case 8:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[8].GetComponent<TextMeshProUGUI>().text);
                break;
            case 9:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[9].GetComponent<TextMeshProUGUI>().text);
                break;
            case 10:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[10].GetComponent<TextMeshProUGUI>().text);
                break;
            case 11:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[11].GetComponent<TextMeshProUGUI>().text);
                break;
            case 12:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[12].GetComponent<TextMeshProUGUI>().text);
                break;
            case 13:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[13].GetComponent<TextMeshProUGUI>().text);
                break;
            case 14:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[14].GetComponent<TextMeshProUGUI>().text);
                break;
            case 15:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[15].GetComponent<TextMeshProUGUI>().text);
                break;
            case 16:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[16].GetComponent<TextMeshProUGUI>().text);
                break;
            case 17:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[17].GetComponent<TextMeshProUGUI>().text);
                break;
            case 18:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[18].GetComponent<TextMeshProUGUI>().text);
                break;
            case 19:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[19].GetComponent<TextMeshProUGUI>().text);
                break;
            case 20:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[20].GetComponent<TextMeshProUGUI>().text);
                break;
            case 21:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[21].GetComponent<TextMeshProUGUI>().text);
                break;
            case 22:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[22].GetComponent<TextMeshProUGUI>().text);
                break;
            case 23:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[23].GetComponent<TextMeshProUGUI>().text);
                break;
            case 24:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[24].GetComponent<TextMeshProUGUI>().text);
                break;
            case 25:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[25].GetComponent<TextMeshProUGUI>().text);
                break;
            case 26:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[26].GetComponent<TextMeshProUGUI>().text);
                break;
            case 27:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[27].GetComponent<TextMeshProUGUI>().text);
                break;
            case 28:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[28].GetComponent<TextMeshProUGUI>().text);
                break;
            case 29:
                form.AddField("value",
                    _mb.GetComponent<MoonbeamDialogue>().artifactQuestions[29].GetComponent<TextMeshProUGUI>().text);
                break;
        }
    }
}