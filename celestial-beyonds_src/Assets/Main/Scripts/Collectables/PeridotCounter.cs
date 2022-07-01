using TMPro;
using UnityEngine;

public class PeridotCounter : MonoBehaviour
{
    public int peridots;
    public TextMeshProUGUI _peridotCounter;

    private void Start()
    {
        peridots = PlayerPrefs.GetInt("peridots");
    }

    // Save peridots to player.
    private void Update()
    {
        PlayerPrefs.SetInt("peridots", peridots);
        if (peridots <= 0)
            peridots = 0;
    }
    
    public void SellPeridots(int amount)
    {
        peridots -= amount;
    }

    // Add the updated peridots amount to _peridotCounter.
    private void OnGUI()
    {
        _peridotCounter.text = "PERIDOTS: " + peridots;
    }
}
