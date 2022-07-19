using UnityEngine;
using UnityEngine.UI;

public class PollinatorAmmo : MonoBehaviour
{
    public GameObject pollenBar;
    public int maxAmmo = 80;
    public int pollenAmmo;
    private Slider _pollenBarSlider;
    
    private void Start()
    {
        pollenAmmo = maxAmmo;
        _pollenBarSlider = pollenBar.GetComponent<Slider>();
    }
    
    private void Update()
    {
        _pollenBarSlider.value = pollenAmmo;
    }
    
    public void FillUpPollen(int amount)
    {
        pollenAmmo += amount;
    }

}
