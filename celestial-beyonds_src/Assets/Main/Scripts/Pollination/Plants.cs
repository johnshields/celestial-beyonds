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
        AudioSource.PlayClipAtPoint(blossomSFX, plantsOG[num].transform.position, 0.5f);
        plantClones[num].SetActive(true);
        Destroy(plantsOG[num]);
        miniMenu.GetComponent<MiniMenu>().plantsNum += 1;
        _pl.GetComponent<PollinationLevel>().IncreasePollination();
        print("Plant cloned and dummy destroyed: " + num);
    }
}