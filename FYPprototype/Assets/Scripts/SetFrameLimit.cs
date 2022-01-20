using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFrameLimit : MonoBehaviour
{
    public int targetFrameRate = 144;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
