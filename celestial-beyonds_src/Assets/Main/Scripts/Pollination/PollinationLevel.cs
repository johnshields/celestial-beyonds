using TMPro;
using UnityEngine;


public class PollinationLevel : MonoBehaviour
{
    public int pollinationPercent, maxPollination = 100;
    public TextMeshProUGUI _pollinationLevel;
    
    private void Start()
    {
        pollinationPercent = PlayerPrefs.GetInt("PollinationLevel");
        pollinationPercent = 0;
    }
    
    private void Update()
    {
        PlayerPrefs.SetInt("PollinationLevel", pollinationPercent);
    }

    public void IncreasePollination()
    {
        pollinationPercent += 25;
    }
    
    private void OnGUI()
    {
        if(pollinationPercent != maxPollination)
            _pollinationLevel.text = "POLLINATION: " + pollinationPercent + "%";
    }
}
