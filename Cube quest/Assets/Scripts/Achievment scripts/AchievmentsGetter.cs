using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class AchievmentsGetter : MonoBehaviour
{
    private const float _infiniteTimeDoText = 10000000000000f;

    [SerializeField] private Transform _achievmentsHolder;
    [SerializeField] private Sprite[] _achievmentImages;
    [SerializeField] private List<bool> _hasAchievment = new();
    [SerializeField] private Sprite _lockedAchievementImage;

    private Text[] _lockedAchievmentNames = new Text[8], _allAchievmentNames = new Text[8];
    private string[] _achievmentNamesString = new string[8];
    public static Action<string, string, Sprite> onNewAchievmentGet;
    private void OnEnable()
    {
        SteveHolder.onAchievmentGet += RevealNewAchievment;
        DayNightCycle.onAchievmentGet += RevealNewAchievment;
        GoldenDiceGetter.onAchievmentGet += RevealNewAchievment;
        ShopButtons.onAchievmentGet += RevealNewAchievment;
        MoneyManager.onAchievmentGet += RevealNewAchievment;
        SteveUpgrade.onAchievmentGet += RevealNewAchievment;
        SteveClickRegister.onAchievmentGet += RevealNewAchievment;
        BoostersButtons.onAchievmentGet += RevealNewAchievment;
        AchievmentsButton.onAchievmentsOpen += OpenAchievments;
        AchievmentsButton.onAchievmentsClose += HideAchievments;
        ResetProgress.Reset += ResetAchievements;
    }
    private void OnDisable()
    {
        SteveHolder.onAchievmentGet -= RevealNewAchievment;
        DayNightCycle.onAchievmentGet -= RevealNewAchievment;
        GoldenDiceGetter.onAchievmentGet -= RevealNewAchievment;
        ShopButtons.onAchievmentGet -= RevealNewAchievment;
        MoneyManager.onAchievmentGet-= RevealNewAchievment;
        SteveUpgrade.onAchievmentGet -= RevealNewAchievment;
        SteveClickRegister.onAchievmentGet -= RevealNewAchievment;
        BoostersButtons.onAchievmentGet -= RevealNewAchievment;
        AchievmentsButton.onAchievmentsOpen -= OpenAchievments;
        AchievmentsButton.onAchievmentsClose -= HideAchievments;
        ResetProgress.Reset -= ResetAchievements;
    }
    private void Start()
    {
        GetAchivementsObjects();
        StartCoroutine(WaitForLanguageChange());
    }
    private void GetAchivementsObjects()
    {
        for (int i = 0; i < _achievmentsHolder.childCount; i++)
        {
            _lockedAchievmentNames[i] = _achievmentsHolder.GetChild(i).GetChild(3).GetComponent<Text>();
            _allAchievmentNames[i] = _lockedAchievmentNames[i];
            _achievmentNamesString[i] = _allAchievmentNames[i].text;
            _allAchievmentNames[i].text = _achievmentNamesString[i];
        }
    }
    private void GetStartingNames()
    {
        for (int i = 0; i < _hasAchievment.Count; i++)
        {
            if (YandexGame.savesData.Achievments[i])
            {
                _hasAchievment[i] = true;
                _lockedAchievmentNames[i].DOKill();
                _lockedAchievmentNames[i] = null;
                _achievmentsHolder.GetChild(i).GetChild(1).GetComponent<Image>().sprite = _achievmentImages[i];
            }
        }
    }
    private IEnumerator WaitForLanguageChange()
    {
        yield return new WaitForSeconds(0.1f);
        GetStartingNames();
    }
    private void OpenAchievments()
    {
        foreach (Text AchievmentName in _lockedAchievmentNames)
        {
            if (AchievmentName != null)
            {
                AchievmentName.DOText("Пасхалка", _infiniteTimeDoText, true, ScrambleMode.Lowercase);
            }
        }
    }
    private void HideAchievments()
    {
        foreach (Text AchievmentName in _allAchievmentNames)
        {
            AchievmentName.DOKill();
        }
    }
    private void RevealNewAchievment(int Index)
    {
        Index--;
        if (_hasAchievment[Index])
            return;
        _hasAchievment[Index] = true;
        YandexGame.savesData.Achievments[Index] = true;
        BoostersButtons.BoostersAmount++;
        Transform Achievment = _achievmentsHolder.GetChild(Index);
        _lockedAchievmentNames[Index].DOKill();
        _lockedAchievmentNames[Index] = null;
        _allAchievmentNames[Index].text = _achievmentNamesString[Index];
        Achievment.GetChild(1).GetComponent<Image>().sprite = _achievmentImages[Index];
        onNewAchievmentGet?.Invoke(Achievment.GetChild(2).GetComponent<Text>().text, Achievment.GetChild(3).GetComponent<Text>().text, _achievmentImages[Index]);
    }
    private void ResetAchievements()
    {
        for (int i = 0; i < _hasAchievment.Count; i++)
        {
            _hasAchievment[i] = false;
            Transform Achievment = _achievmentsHolder.GetChild(i);
            Achievment.GetChild(1).GetComponent<Image>().sprite = _lockedAchievementImage;
            YandexGame.savesData.Achievments[i] = false;
            _lockedAchievmentNames[i] = _allAchievmentNames[i];
            _lockedAchievmentNames[i].DOText("Пасхалка", _infiniteTimeDoText, true, ScrambleMode.Lowercase);
        }
    }
}
