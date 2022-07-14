using System.Collections;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject[] plantsOG, plantClones;
    public GameObject miniMenu;
    public AudioClip blossomSFX;
    private GameObject _pl;
    private AudioSource _audio;

    private void Start()
    {
        _pl = GameObject.FindGameObjectWithTag("PollinationLevel");
        _audio = GetComponent<AudioSource>();
    }

    public void Blossom(int num)
    {
        _audio.PlayOneShot(blossomSFX);
        plantClones[num].SetActive(true);
        Destroy(plantsOG[num]);
        miniMenu.GetComponent<MiniMenu>().plantsNum += 1;
        _pl.GetComponent<PollinationLevel>().IncreasePollination();
    }
}