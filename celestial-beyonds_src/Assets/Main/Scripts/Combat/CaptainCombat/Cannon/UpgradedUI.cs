using UnityEngine;

public class UpgradedUI : MonoBehaviour
{
    public GameObject cannon, pepperbox, defier;

    private void Update()
    {
        if (Booleans.pepperBoxUpgraded)
        {
            cannon.SetActive(false);
            pepperbox.SetActive(true);
        }
        else if (Booleans.celestialDeferUpgraded)
        {
            pepperbox.SetActive(false);
            defier.SetActive(true);
        }
    }
}
