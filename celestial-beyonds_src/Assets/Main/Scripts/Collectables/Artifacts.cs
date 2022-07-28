using System.Text.RegularExpressions;
using UnityEngine;

public class Artifacts : MonoBehaviour
{
    private GameObject _player;
    private string _parentName;
    public bool interaction, vanInScene;
    public GameObject moonbeamAskPrompt, mb;
    public string artifact;
    public int triggeredArtifact;
    public AudioClip vincentBark;

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

            if (gameObject.name == "ArtifactTrigger (6)" && vanInScene)
                AudioSource.PlayClipAtPoint(vincentBark, other.transform.position, 0.01f);
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