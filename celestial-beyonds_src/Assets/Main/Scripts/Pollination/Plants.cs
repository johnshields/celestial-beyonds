using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject[] plantsOG, plantClones;
    private GameObject _pl;

    private void Start()
    {
        _pl = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    public void Blossom(int num)
    {
        plantClones[num].SetActive(true);
        Destroy(plantsOG[num]);
        _pl.GetComponent<PollinationLevel>().IncreasePollination();
    }
}