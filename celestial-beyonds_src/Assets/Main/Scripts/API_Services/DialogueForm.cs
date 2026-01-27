using TMPro;
using UnityEngine;

public class DialogueForm : MonoBehaviour
{
    [SerializeField] private string moonbeamTag = "Moonbeam";

    private MoonbeamDialogue _dialogue;

    private void Awake()
    {
        var mb = GameObject.FindGameObjectWithTag(moonbeamTag);
        _dialogue = mb != null ? mb.GetComponent<MoonbeamDialogue>() : null;

        if (_dialogue == null)
            Debug.LogError($"DialogueForm: Could not find MoonbeamDialogue on tag '{moonbeamTag}'.");
    }

    public void SetUpForm(WWWForm form)
    {
        if (_dialogue == null || form == null) return;

        // General
        if (_dialogue.generalQ && !_dialogue.artifactQ)
        {
            int index = _dialogue.whichDialogue - 100;
            AddValueFromOptions(form, _dialogue.dialogueOptions, index);
            return;
        }

        // Artifacts
        if (!_dialogue.generalQ && _dialogue.artifactQ)
        {
            int index = _dialogue.whichDialogue;
            AddValueFromOptions(form, _dialogue.artifactQuestions, index);
        }
    }

    private static void AddValueFromOptions(WWWForm form, GameObject[] options, int index)
    {
        if (options == null || index < 0 || index >= options.Length) return;

        var tmp = options[index].GetComponent<TextMeshProUGUI>();
        if (tmp == null) return;

        form.AddField("value", tmp.text);
    }
}
