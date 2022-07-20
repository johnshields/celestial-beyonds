using UnityEngine;

public class TrappistIntro : MonoBehaviour
{
    public GameObject syncCin, ship, characters;

    private void Update()
    {
        if (syncCin.GetComponent<SyncCinematic>().cinStarted)
        {
            print("CinStarted: " + syncCin.GetComponent<SyncCinematic>().cinStarted);
            ship.SetActive(true);
            characters.SetActive(true);
        }
    }
}