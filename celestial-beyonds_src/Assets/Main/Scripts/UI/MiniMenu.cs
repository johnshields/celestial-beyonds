using System.Collections;
using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemiesNum,
        plantsNum,
        artifactsNum,
        peridotsNum,
        totalEnemies,
        totalPlants,
        totalArtifacts,
        totalPeridot;

    public TextMeshProUGUI plantNumTxt, enemyNumTxt, artifactsNumTxt, peridotsNumTxt;
    public GameObject allEnemiesPanel, allArtifactsPanel, allPeridotsPanel;
    private bool _allEnemies, allArtifacts, allPeridots;

    private void OnGUI()
    {
        if (plantsNum != 0 || enemiesNum != 0 || artifactsNum != 0 || artifactsNum != 0 || peridotsNum != 0)
        {
            plantNumTxt.text = plantsNum + " / " + totalPlants;
            enemyNumTxt.text = enemiesNum + " / " + totalEnemies;
            artifactsNumTxt.text = artifactsNum + " / " + totalArtifacts;
            peridotsNumTxt.text = peridotsNum + " / " + totalPeridot;
        }
    }

    private void Update()
    {
        if (enemiesNum == 10 && !_allEnemies)
        {
            allEnemiesPanel.SetActive(true);
            _allEnemies = true;
            StartCoroutine(CloseActivePanel(0));
        }
        else if (artifactsNum == 10 && !allArtifacts)
        {
            allArtifactsPanel.SetActive(true);
            allArtifacts = true;
            StartCoroutine(CloseActivePanel(1));
        }
        else if (peridotsNum == 66 && !allPeridots)
        {
            allPeridotsPanel.SetActive(true);
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
                allEnemiesPanel.SetActive(false);
                break;
            case 1:
                allArtifactsPanel.SetActive(false);
                break;
            case 3:
                allPeridotsPanel.SetActive(false);
                break;
        }
    }
}