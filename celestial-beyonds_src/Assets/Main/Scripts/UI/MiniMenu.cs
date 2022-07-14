using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemiesNum, plantsNum, artifactsNum, peridotsNum, totalEnemies, totalPlants, totalArtifacts, totalPeridots;
    public TextMeshProUGUI plantNumTxt, enemyNumTxt, artifactsNumTxt, peridotsNumTxt;
    public GameObject allEnemiesPanel, allArtifactsPanel, allPeridotsPanel;
    public AudioClip[] achievementSFX;
    private AudioSource _audio;
    private bool _allEnemies, allArtifacts, allPeridots;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        if (plantsNum != 0 || enemiesNum != 0 || artifactsNum != 0 || artifactsNum != 0 || peridotsNum != 0)
        {
            plantNumTxt.text = plantsNum + " / " + totalPlants;
            enemyNumTxt.text = enemiesNum + " / " + totalEnemies;
            artifactsNumTxt.text = artifactsNum + " / " + totalArtifacts;
            peridotsNumTxt.text = peridotsNum + " / " + totalPeridots;
        }
    }

    private void Update()
    {
        if (enemiesNum == 10 && !_allEnemies)
        {
            _allEnemies = true;
            StartCoroutine(CloseActivePanel(0));
        }
        else if (artifactsNum == 10 && !allArtifacts)
        {
            allArtifacts = true;
            StartCoroutine(CloseActivePanel(1));
        }
        else if (peridotsNum == 66 && !allPeridots)
        {
            allPeridots = true;
            StartCoroutine(CloseActivePanel(2));
        }
    }

    private IEnumerator CloseActivePanel(int whichPanel)
    {
        yield return new WaitForSeconds(3);
        switch (whichPanel)
        {
            case 0:
                allEnemiesPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[0]);
                break;
            case 1:
                allArtifactsPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[1]);
                break;
            case 2:
                allPeridotsPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[1]);
                break;
        }

        yield return new WaitForSeconds(3);
        switch (whichPanel)
        {
            case 0:
                allEnemiesPanel.SetActive(false);
                break;
            case 1:
                allArtifactsPanel.SetActive(false);
                break;
            case 2:
                allPeridotsPanel.SetActive(false);
                break;
        }
    }
}