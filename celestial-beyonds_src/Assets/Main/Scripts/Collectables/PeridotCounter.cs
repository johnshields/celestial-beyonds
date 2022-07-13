using TMPro;
using UnityEngine;

public class PeridotCounter : MonoBehaviour
{
    public int peridots;
    public TextMeshProUGUI _peridotCounter;

    private void Awake()
    {
        peridots = PlayerPrefs.GetInt("peridots");
        peridots = 0;
    }

    // Save peridots to player.
    private void Update()
    {
        PlayerPrefs.SetInt("peridots", peridots);
        if (peridots <= 0)
            peridots = 0;
    }

    // Add the updated peridots amount to _peridotCounter.
    private void OnGUI()
    {
        _peridotCounter.text = "PERIDOTS: " + peridots;
    }

    public void SellPeridots(int amount)
    {
        peridots -= amount;
    }
}