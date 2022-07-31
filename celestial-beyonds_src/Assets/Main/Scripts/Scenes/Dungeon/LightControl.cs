using System.Collections;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public GameObject puzzle;
    public bool lightActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !lightActivated)
        {
            lightActivated = true;   
            puzzle.GetComponent<DungeonPuzzle>().ActivateLight();
            StartCoroutine(OpenDungeon());
        }
    }

    private IEnumerator OpenDungeon()
    {
        yield return new WaitForSeconds(3);
        puzzle.GetComponent<DungeonPuzzle>().OpenDungeon();
        print("Dungeon Open!");
    }
}
