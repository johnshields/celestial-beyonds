using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UIActionsProfiler _uiActionsProfiler;

    private void Awake()
    {
        _uiActionsProfiler = new UIActionsProfiler();
    }
    
}
