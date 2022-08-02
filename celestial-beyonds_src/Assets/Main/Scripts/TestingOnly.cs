using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class TestingOnly : MonoBehaviour
{
    public bool enablePepperBox, enableDefier, enableArmor, lotsOfPeridots, lotsOfAmmo, lotsOfPollen, fog;
    public GameObject pc, ca, pa, player;
    
    // Testing Cheats
    private void Start()
    {
#if UNITY_EDITOR
        RenderSettings.fog = fog;
        
        if (enablePepperBox)
        {
            Bools.pbUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().pbUpgrade = true;
        }

        if (enableDefier)
        {
            Bools.cdUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
        }

        if (enableArmor)
        {
            Bools.aUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().aUpgrade = true;
            StartCoroutine(AddHealth());
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

    private IEnumerator AddHealth()
    {
        yield return new WaitForSeconds(3);
        player.GetComponent<CaptainHealth>().currentHealth = 200;
    }
}
