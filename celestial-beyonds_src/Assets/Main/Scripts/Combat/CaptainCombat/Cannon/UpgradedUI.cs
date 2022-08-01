using UnityEngine;

public class UpgradedUI : MonoBehaviour
{
    public GameObject cannon, pepperbox, defier;

    private void Update()
    {
        if (Bools.pbUpgraded)
        {
            cannon.SetActive(false);
            pepperbox.SetActive(true);
        }
        else if (Bools.cdUpgraded)
        {
            pepperbox.SetActive(false);
            defier.SetActive(true);
        }
    }
}
