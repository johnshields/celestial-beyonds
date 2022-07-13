using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemiesNum, plantsNum, artifactsNum, totalEnemies, totalPlants, totalArtifacts;
    public TextMeshProUGUI plantNumTxt, enemyNumTxt, artifactsNumTxt;

    private void OnGUI()
    {
        if (plantsNum != 0 || enemiesNum != 0 || artifactsNum != 0)
        {
            plantNumTxt.text = plantsNum + " / " + totalPlants;
            enemyNumTxt.text = enemiesNum + " / " + totalEnemies;
            artifactsNumTxt.text = artifactsNum + " / " + totalArtifacts;
        }
    }
}