using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject[] _plantsOG, _plantClones;
    private GameObject pl;

    private void Start()
    {
        pl = GameObject.FindGameObjectWithTag("PollinationLevel");
    }

    public void Blossom(int num)
    {
        _plantClones[num].SetActive(true);
        Destroy(_plantsOG[num]);
        pl.GetComponent<PollinationLevel>().IncreasePollination();
    }
}