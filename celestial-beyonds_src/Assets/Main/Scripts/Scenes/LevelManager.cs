using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject enemies, fader;
    public int time;

    private void Start()
    {
        fader.GetComponent<Animator>().SetBool("FadeIn", true);
        fader.GetComponent<Animator>().SetBool("FadeOut", false);
        StartCoroutine(SetActiveObjects());

        if (time != 0)
        {
            StartCoroutine(FadeSceneOut());
        }
    }

    private IEnumerator SetActiveObjects()
    {
        yield return new WaitForSeconds(3);
        enemies.SetActive(true);
    }
    
    private IEnumerator FadeSceneOut()
    {
        yield return new WaitForSeconds(time);
        fader.GetComponent<Animator>().SetBool("FadeIn", false);
        fader.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}