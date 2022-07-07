using TMPro;
using UnityEngine;

public class PollinationLevel : MonoBehaviour
{
    public int pollinationPercent, maxPollination = 100;
    public TextMeshProUGUI _pollinationLevel;
    public GameObject levelCompleteUI;
    public bool levelCompleted;

    private void Start()
    {
        pollinationPercent = PlayerPrefs.GetInt("PollinationLevel");
        if (!levelCompleted)
            pollinationPercent = 0;
        levelCompleteUI.SetActive(false);
    }

    private void Update()
    {
        PlayerPrefs.SetInt("PollinationLevel", pollinationPercent);
    }

    private void OnGUI()
    {
        if (pollinationPercent <= maxPollination)
            _pollinationLevel.text = "POLLINATION: " + pollinationPercent + "%";
    }

    public void IncreasePollination()
    {
        pollinationPercent += 25;

        if (pollinationPercent == maxPollination)
        {
            // level complete
            levelCompleted = true;
            print("Level complete! " + levelCompleted);
            levelCompleteUI.SetActive(true);
        }
    }
}