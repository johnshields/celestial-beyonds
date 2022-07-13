using System.Text.RegularExpressions;
using UnityEngine;

public class Artifacts : MonoBehaviour
{
    private GameObject _player;
    private string _parentName;
    public bool interaction;
    public GameObject moonbeamAskPrompt, mb, miniMenu;
    public string artifact;
    public int triggeredArtifact;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        mb = GameObject.FindGameObjectWithTag("Moonbeam");
        _parentName = transform.parent.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player)
        {
            interaction = true;
            moonbeamAskPrompt.SetActive(true);
            // get objNum
            artifact = Regex.Replace(gameObject.name, "[^0-9]", "");
            triggeredArtifact = int.Parse(artifact);
            mb.GetComponent<MoonbeamDialogue>().artifactNum = triggeredArtifact;
            print("interaction w/ Artifact " + triggeredArtifact + " :" 
                  + _parentName + ": " + interaction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player)
        {
            interaction = false;
            moonbeamAskPrompt.SetActive(false);
        }
    }
}