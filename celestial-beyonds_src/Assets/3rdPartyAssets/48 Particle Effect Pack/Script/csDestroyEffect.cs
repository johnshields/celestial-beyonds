﻿using UnityEngine;

public class csDestroyEffect : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
            Destroy(gameObject);
    }
}