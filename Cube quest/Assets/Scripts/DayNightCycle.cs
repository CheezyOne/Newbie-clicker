using System;
using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 0.5f;
    [SerializeField] private float _timeBeforeAchievment = 720f;
    public static Action<int> onAchievmentGet;
    public static bool StopTheSun;
    private void Awake()
    {
        StartCoroutine(FullDay());
    }
    private void Update()
    {
        if (StopTheSun)
            return;
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
    private IEnumerator FullDay()
    {
        yield return new WaitForSeconds(_timeBeforeAchievment);
        onAchievmentGet?.Invoke(1);
        yield break;
    }
}
