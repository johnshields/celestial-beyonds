using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject[] plantsOG, plantClones;
    public GameObject miniMenu;
    public AudioClip blossomSFX;
    private GameObject _pl;

    private void Start()
    {
        _pl = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    public void Blossom(int num)
    {
        // Blossom Plant
        AudioSource.PlayClipAtPoint(blossomSFX, plantsOG[num].transform.position, 0.5f);
        Destroy(plantsOG[num]);
        plantClones[num].SetActive(true);
        plantClones[num].GetComponentInChildren<Light>().enabled = true;
        // IncreasePollination.
        miniMenu.GetComponent<MiniMenu>().plantsNum += 1;
        _pl.GetComponent<PollinationLevel>().IncreasePollination();
        print("Plant cloned and dummy destroyed: " + num);
    }
    
}