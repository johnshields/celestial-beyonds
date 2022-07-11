using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemyNum, plantNum;
    private TextMeshProUGUI _plantNumTxt, _enemyNumTxt;

    private void Start()
    {
        _plantNumTxt = GameObject.FindGameObjectWithTag("PlantNum").GetComponent<TextMeshProUGUI>();
        _enemyNumTxt = GameObject.FindGameObjectWithTag("EnemyNum").GetComponent<TextMeshProUGUI>();
    }

    private void OnGUI()
    {
        _plantNumTxt.text = plantNum + " / 20";
        _enemyNumTxt.text = enemyNum + " / 10";
    }
}
