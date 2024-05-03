using System;
using UnityEngine;

public class AchievmentsButton : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera, _achievmentsCamera, _mainCanvas, _achievmentCanvas;
    public static Action onAchievmentsOpen, onAchievmentsClose;
    public void ChangeCameraToAchievments()
    {
        DayNightCycle.StopTheSun = true;
        _mainCamera.SetActive(false);
        _mainCanvas.SetActive(false);
        _achievmentCanvas.SetActive(true);
        _achievmentsCamera.SetActive(true);
        onAchievmentsOpen?.Invoke();
    }
    public void ChangeCameraToMain()
    {
        DayNightCycle.StopTheSun = false;
        _mainCamera.SetActive(true);
        _mainCanvas.SetActive(true);
        _achievmentCanvas.SetActive(false);
        _achievmentsCamera.SetActive(false);
        onAchievmentsClose?.Invoke();
    }
}
