using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject enemies, fader;

    private void Start()
    {
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
        StartCoroutine(SetActiveObjects());
    }

    private IEnumerator SetActiveObjects()
    {
        yield return new WaitForSeconds(3);
        enemies.SetActive(true);
    }
}