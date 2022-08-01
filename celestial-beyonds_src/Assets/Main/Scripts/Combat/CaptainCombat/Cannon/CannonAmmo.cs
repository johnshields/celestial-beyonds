using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonAmmo : MonoBehaviour
{
    public GameObject cannonBar, pbUI, cdUI, cUI, vvg;
    public GameObject handle;
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

        if (cannonAmmo == 0)
        {
            handle.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        else
        {
            if(!vvg.GetComponent<VanGunProfiler>().transaction)
                handle.GetComponent<Image>().color = new Color32(255, 255, 255, 255);   
        }

    }

    public void FillUpCannon(int amount)
    {
        cannonAmmo += amount;
    }
}