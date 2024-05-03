using System;
using System.Collections;
using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private string _leaderboardName;
    [SerializeField] private float _leaderboardUpdateTime;
    [SerializeField] private MoneyManager _moneyManager;

    private void Start()
    {
        StartCoroutine(WaitingLeaderboardCoroutine());
    }
    private IEnumerator WaitingLeaderboardCoroutine()
    {
        yield return new WaitForSeconds(_leaderboardUpdateTime);
        SetRecord();
        yield return WaitingLeaderboardCoroutine();
    }
    private void SetRecord()
    {
        int NewRecord;
        if (_moneyManager.MoneyAmount*10 > int.MaxValue)
            NewRecord = int.MaxValue;
        else
            NewRecord = Convert.ToInt32(_moneyManager.MoneyAmount) / 10;
        YandexGame.NewLeaderboardScores(_leaderboardName, NewRecord);
    }
    private void OnApplicationPause(bool pause)
    {
        YandexGame.SaveProgress();
    }
    private void OnApplicationQuit()
    {
        YandexGame.SaveProgress();
    }
}
