using UnityEngine;

public class ArtifactCounter : MonoBehaviour
{
    private bool _a, _b, _c, _d, _e, _f, _g, _h, _i, _j;
    public GameObject[] aQsHolder;
    public GameObject miniMenu;

    private void Update()
    {
        if (aQsHolder[0].activeInHierarchy && !_a)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _a = true;
        }
        else if (aQsHolder[1].activeInHierarchy && !_b)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _b = true;
        }
        else if (aQsHolder[2].activeInHierarchy && !_c)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _c = true;
        }
        else if (aQsHolder[3].activeInHierarchy && !_d)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _d = true;
        }
        else if (aQsHolder[4].activeInHierarchy && !_e)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _e = true;
        }
        else if (aQsHolder[5].activeInHierarchy && !_f)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _f = true;
        }
        else if (aQsHolder[6].activeInHierarchy && !_g)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _g = true;
        }
        else if (aQsHolder[7].activeInHierarchy && !_h)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _h = true;
        }
        else if (aQsHolder[8].activeInHierarchy && !_i)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _i = true;
        }
        else if (aQsHolder[9].activeInHierarchy && !_j)
        {
            miniMenu.GetComponent<MiniMenu>().artifactsNum += 1;
            _j = true;
        }
    }
}