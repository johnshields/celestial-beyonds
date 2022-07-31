using System.Collections;
using TMPro;
using UnityEngine;

public class PuzzleInit : MonoBehaviour
{
    public bool initPuzzle;
    public TextMeshProUGUI mbDialogue;
    public GameObject mbAPI, lightControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !lightControl.GetComponent<LightControl>().lightActivated)
        {
            initPuzzle = true;
            print("initPuzzle: " + initPuzzle);
            mbDialogue.text = "What lies beyond that Wall? \nWhat does that Light do?";
            mbAPI.GetComponent<MoonbeamAPI>().PlayRandomClip(0.5f);
            StartCoroutine(CloseDialogue());
        }
    }

    private IEnumerator CloseDialogue()
    {
        yield return new WaitForSeconds(5);
        mbDialogue.text = "";
    }
}
