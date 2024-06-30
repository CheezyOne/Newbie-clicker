using System;
using System.Collections;
using UnityEngine;

public class SteveClickRegister : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private int _clickCounterForAchievment=0;

    public static Action<int> onAchievmentGet;
    public static Action onSteveClick;

    private const int _clicksNeedForAchievment=8;
    private const float _achievmentTime = 1f;
    private void Update()
    {
        if (!_mainCamera.gameObject.activeSelf )
            return;
        if (Input.GetMouseButtonDown(0))
        {
            GetClick();
        }
    }
    private IEnumerator AchievmentClickTimer()
    {
        if (_clickCounterForAchievment > _clicksNeedForAchievment)
        {
            onAchievmentGet?.Invoke(3);
        }
        yield return new WaitForSeconds(_achievmentTime);
        _clickCounterForAchievment--;
    }
    private void GetClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition); 

        if (Physics.Raycast(ray, out RaycastHit hit))  
        {
            if(hit.transform.GetComponent<SteveHolder>()!=null)
            {
                    _clickCounterForAchievment++;
                StartCoroutine(AchievmentClickTimer());
                onSteveClick?.Invoke();
            }
        }
    }
}
