using UnityEngine;

public class PauseGameWithTimer : MonoBehaviour
{
    private void OnEnable()
    {
        AudioListener.pause = true; Time.timeScale = 0; Cursor.visible = true; Cursor.lockState = CursorLockMode.Locked;
    }    
}
