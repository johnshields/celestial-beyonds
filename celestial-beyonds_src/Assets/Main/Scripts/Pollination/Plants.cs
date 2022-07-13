using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject[] plantsOG, plantClones;
    private GameObject _pl;
    public GameObject miniMenu;

    private void Start()
    {
        _pl = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    public void Blossom(int num)
    {
        miniMenu.GetComponent<MiniMenu>().plantsNum += 1;
        plantClones[num].SetActive(true);
        Destroy(plantsOG[num]);
        _pl.GetComponent<PollinationLevel>().IncreasePollination();
    }
}