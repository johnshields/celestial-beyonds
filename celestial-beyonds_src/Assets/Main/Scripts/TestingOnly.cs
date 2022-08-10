using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;

public class TestingOnly : MonoBehaviour
{
    public bool enablePepperBox, enableDefier, enableArmor, lotsOfPeridots, lotsOfAmmo, lotsOfPollen, fog;
    public GameObject ca, pa, player;
    
    // Testing Cheats
    private void Update()
    {
#if UNITY_EDITOR
        RenderSettings.fog = fog;
        
        if (enablePepperBox)
        {
            Bools.pbUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().pbUpgrade = true;
            PlayerMemory.cannonUpgrade = 1;
        }

        if (enableDefier)
        {
            Bools.cdUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
            PlayerMemory.cannonUpgrade = 2;
        }

        if (enableArmor)
        {
            Bools.aUpgraded = true;
            player.GetComponent<CaptainAnimAndSound>().aUpgrade = true;
            StartCoroutine(AddHealth());
        }

        if (lotsOfPeridots)
        {
            PlayerMemory.peridots = 500;
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
        if (player.GetComponent<CaptainHealth>().currentHealth != 200)
        {
            yield return new WaitForSeconds(2);
            player.GetComponent<CaptainHealth>().currentHealth = 200;   
        }
    }
}
