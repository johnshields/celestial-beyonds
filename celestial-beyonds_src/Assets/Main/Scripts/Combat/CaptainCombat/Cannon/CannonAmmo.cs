using UnityEngine;
using UnityEngine.UI;

public class CannonAmmo : MonoBehaviour
{
    public GameObject cannonBar;
    public int maxAmmo = 100;
    public int cannonAmmo;
    private Slider _cannonBarSlider;

    private void Start()
    {
        cannonAmmo = maxAmmo;
        _cannonBarSlider = cannonBar.GetComponent<Slider>();
    }

    private void Update()
    {
        _cannonBarSlider.value = cannonAmmo;
        
        if (cannonAmmo < 0)
            cannonAmmo = 0;
    }

    public void FillUpCannon(int amount)
    {
        cannonAmmo += amount;
    }
}