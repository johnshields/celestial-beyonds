using System.Text.RegularExpressions;
using UnityEngine;

public class Artifacts : MonoBehaviour
{
    private GameObject _player;
    private string _numStrip;
    private int _objNum;
    public bool interaction;
    public GameObject moonbeamAskPrompt, moonbeamAPI;
    private string _parentName;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        moonbeamAPI = GameObject.FindGameObjectWithTag("Moonbeam");
        _parentName = transform.parent.name;
        _numStrip = Regex.Replace(gameObject.name, "[^0-9]", "");
        _objNum = int.Parse(_numStrip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            interaction = true;
            moonbeamAPI.GetComponent<MoonbeamDialogue>().asking = true;
            moonbeamAskPrompt.SetActive(true);
            SwitchArtifact(_parentName);
            print("interaction w/ Artifact " + _parentName + ": " + interaction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            interaction = false;
            //moonbeamAPI.GetComponent<MoonbeamDialogue>().asking = false;
            moonbeamAskPrompt.SetActive(false);
            SwitchArtifact(_parentName);
            print("interaction w/ Artifact " + _parentName + ": " + interaction);
        }
    }

    private void SwitchArtifact(string objName)
    {
        switch (_objNum)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                print(objName);
                break;
        }
    }
}