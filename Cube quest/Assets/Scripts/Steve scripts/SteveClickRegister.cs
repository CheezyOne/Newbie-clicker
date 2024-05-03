using System;
using System.Collections;
using UnityEngine;

public class SteveClickRegister : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private int _clickCounterForAchievment=0;
    private bool _gotAchievment;

    public static Action<int> onAchievmentGet;
    public static Action onSteveClick;

    private const int _clicksNeedForAchievment=9;
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
        if (_clickCounterForAchievment > _clicksNeedForAchievment && !_gotAchievment)
        {
            onAchievmentGet?.Invoke(3);
            _gotAchievment = true;
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
                if (!_gotAchievment)
                {
                    _clickCounterForAchievment++;
                    StartCoroutine(AchievmentClickTimer());
                }
                onSteveClick?.Invoke();
            }
        }
    }
}
