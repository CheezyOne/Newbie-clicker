using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BoostersButtons : MonoBehaviour
{
    public static int BoostersAmount = 0;
    [SerializeField] private GameObject[] _boostButtons;
    [SerializeField] private GameObject _doubleMoneyBlocker;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private TMP_Text _boostersNumber, _doubleMoneyBlockerTimer;
    private bool _hasDoubleTick, _hasDoubleClick, _hasDoubleXP;
    public static Action onXPBoostActive, onXPBoostDeactive;
    public static Action<int> onBoostAdStart;
    public static Action<int> onAchievmentGet;
    private int _doubleMoneyTimer = 0;
    private void OnEnable()
    {
        if (_doubleMoneyTimer != 0)
            StartCoroutine(DoubleMoneyTimer());
        YandexGame.RewardVideoEvent += StartBoost;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= StartBoost;
    }
    private void Start()
    {
        if (YandexGame.savesData.isFirstSession)
            return;
        BoostersAmount = YandexGame.savesData.BoostersAmount;
    }
    private void StartBoost(int BoostIndex)
    {
        onAchievmentGet?.Invoke(5);
        switch (BoostIndex)
        {
            case 0:
                {
                    ClickBoost();
                    break;
                }
            case 1:
                {
                    TickBoost();
                    break;
                }
            case 2:
                {
                    XPBoost();
                    break;
                }
            case 3:
                {
                    DoubleTheMoney();
                    break;
                }
        }
    }
    private void Update()
    {
        YandexGame.savesData.BoostersAmount = BoostersAmount;
        _boostersNumber.text = BoostersAmount.ToString();
    }

    public void StartBoostAd(int BoostIndex)
    {
        AchievmentGet();
        if (BoostersAmount <= 0)
        {
            onBoostAdStart?.Invoke(BoostIndex);
            return;
        }
        else
        {
            BoostersAmount--;
            StartBoost(BoostIndex);
        }
    }
    private void ClickBoost()
    {
        DeactivateButtons();
        _boostButtons[0].transform.DOScale(1.3f, 0.3f).SetLoops(96, LoopType.Yoyo);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_boostButtons[0].transform.GetChild(1).GetComponent<Image>().DOFillAmount(0, 30));
        sequence.Append(_boostButtons[0].transform.GetChild(1).GetComponent<Image>().DOFillAmount(100, 0));
        sequence.Play();
        sequence.OnComplete(ActivateButtons);
        _moneyManager.DoubleClick();
        _hasDoubleClick = true;
    }
    private void TickBoost()
    {
        DeactivateButtons();
        _boostButtons[1].transform.DOScale(1.3f, 0.3f).SetLoops(96, LoopType.Yoyo);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_boostButtons[1].transform.GetChild(1).GetComponent<Image>().DOFillAmount(0, 30));
        sequence.Append(_boostButtons[1].transform.GetChild(1).GetComponent<Image>().DOFillAmount(100, 0));
        sequence.Play();
        sequence.OnComplete(ActivateButtons);
        _moneyManager.DoubleTick();
        _hasDoubleTick = true;
    }
    private void XPBoost()
    {
        DeactivateButtons();
        _boostButtons[2].transform.DOScale(1.3f, 0.3f).SetLoops(96, LoopType.Yoyo);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_boostButtons[2].transform.GetChild(1).GetComponent<Image>().DOFillAmount(0, 30));
        sequence.Append(_boostButtons[2].transform.GetChild(1).GetComponent<Image>().DOFillAmount(100, 0));
        sequence.Play();
        sequence.OnComplete(ActivateButtons);
        onXPBoostActive?.Invoke();
        _hasDoubleXP = true;
    }
    private IEnumerator DoubleMoneyTimer()
    {
        _doubleMoneyTimer = Convert.ToInt32(_doubleMoneyBlockerTimer.text);
        if (_doubleMoneyTimer == 0)
        {
            _boostButtons[3].GetComponent<Button>().enabled = true;
            _doubleMoneyBlocker.SetActive(false);
            _doubleMoneyBlockerTimer.text = "30";
            yield break;
        }
        yield return new WaitForSeconds(1);
        _doubleMoneyTimer--;
        _doubleMoneyBlockerTimer.text = Convert.ToString(_doubleMoneyTimer);
        yield return DoubleMoneyTimer();
    }
    private void DoubleTheMoney()
    {
        _doubleMoneyBlocker.SetActive(true);
        _boostButtons[3].GetComponent<Button>().enabled = false;
        StartCoroutine(DoubleMoneyTimer());
        _moneyManager.MoneyAmount *= 2;
    }
    private void AchievmentGet()
    {
        if (BoostersAmount <= 0)
            return;
        onAchievmentGet?.Invoke(5);
    }
    private void DeactivateButtons()
    {
        foreach (GameObject Button in _boostButtons)
        {
            Button.GetComponent<Button>().interactable = false;
        }
    }
    private void ActivateButtons()
    {
        foreach (GameObject Button in  _boostButtons)
        {
            Button.GetComponent<Button>().interactable = true;
        }
        SetVariablesBack();
    }
    private void SetVariablesBack()
    {
        if(_hasDoubleTick)
        {
            _hasDoubleTick = false;
            _moneyManager.HalfTick();
            return;
        }
        if(_hasDoubleClick)
        {
            _hasDoubleClick = false;
            _moneyManager.HalfClick();
            return;
        }
        if(_hasDoubleXP)
        {
            _hasDoubleXP = false;
            onXPBoostDeactive?.Invoke();
            return;
        }
    }
    public void ResetBoosters()
    {
        BoostersAmount = 0;
        YandexGame.savesData.BoostersAmount = 0;
    }
}
