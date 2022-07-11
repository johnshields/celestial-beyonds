using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemyNum, plantNum, totalEnemies, totalPlants;
    public TextMeshProUGUI plantNumTxt, enemyNumTxt;

    private void OnGUI()
    {
        if (plantNum != 0 || enemyNum != 0)
        {
            plantNumTxt.text = plantNum + " / " + totalPlants;
            enemyNumTxt.text = enemyNum + " / " + totalEnemies;   
        }
    }
}
