using UnityEngine;

public class SetFrameLimit : MonoBehaviour
{
    public int targetFrameRate = 144;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
