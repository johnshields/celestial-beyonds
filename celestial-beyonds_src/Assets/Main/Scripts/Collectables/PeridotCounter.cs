using TMPro;
using UnityEngine;

public class PeridotCounter : MonoBehaviour
{
    public TextMeshProUGUI _peridotCounter;

    // Save peridots to player.
    private void Update()
    {
        if (PlayerMemory.peridots <= 0)
            PlayerMemory.peridots = 0;
    }

    // Add the updated peridots amount to _peridotCounter.
    private void OnGUI()
    {
        _peridotCounter.text = "PERIDOTS: " + PlayerMemory.peridots;
    }

    public void SellPeridots(int amount)
    {
        PlayerMemory.peridots -= amount;
    }
}