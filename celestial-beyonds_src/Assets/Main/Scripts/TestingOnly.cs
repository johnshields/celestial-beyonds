using Main.Scripts.Captain;
using UnityEngine;

public class TestingOnly : MonoBehaviour
{
    public bool enablePepperBox, enableDefier, lotsOfPeridots, lotsOfAmmo, lotsOfPollen, fog;
    public GameObject pc, ca, pa, player;
    
    // Testing Cheats
    private void Start()
    {
#if UNITY_EDITOR
        RenderSettings.fog = fog;
        
        if (enablePepperBox)
        {
            Booleans.pepperBoxUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().pepperBoxUpgrade = true;
        }

        if (enableDefier)
        {
            Booleans.celestialDeferUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().celestialDefierUpgrade = true;
        }

        if (lotsOfPeridots)
        {
            pc.GetComponent<PeridotCounter>().peridots = 500;
        }

        if (lotsOfAmmo)
        {
            ca.GetComponent<CannonAmmo>().cannonAmmo = 500;
        }
        
        if (lotsOfPollen)
        {
            pa.GetComponent<PollinatorAmmo>().pollenAmmo = 500;
        }
#endif
    }
}
