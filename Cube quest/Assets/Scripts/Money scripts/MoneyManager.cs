using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MoneyManager : MonoBehaviour
{
    public static Action<int> onAchievmentGet;
    public float MoneyAmount=0;
    [SerializeField] private TMP_Text _moneyText, _tickText;
    [SerializeField] private Transform _textSpawnPosition, _clickCanvas, _blueTextSpawnPosition;
    [SerializeField] private GameObject _clickText;
    private float _moneyClickMultiplier = 1, _moneyPerTick=0;
    private bool _hasAchiemvent, _clickMoneyIsDoubled, _tickMoneyIsDoubled;
    private const float _theRichestManMoney = 160000000000;
    private void OnEnable()
    {
        DiceTargetManager.onMoneyIncrease += ClickMoneyGain;
        GoldenDiceGetter.onGoldenDicePop += GoldenDicePop;
        ShopButtons.onDecreaseMoney += SpendMoney;
        DiceTargetManager.onClickIncrease += IncreaseClickMoney;
        DiceTargetManager.onTickIncrease += IncreaseTickMoney;
    }
    private void OnDisable()
    {
        DiceTargetManager.onMoneyIncrease -= ClickMoneyGain;
        GoldenDiceGetter.onGoldenDicePop -= GoldenDicePop;
        ShopButtons.onDecreaseMoney -= SpendMoney;
        DiceTargetManager.onClickIncrease -= IncreaseClickMoney;
        DiceTargetManager.onTickIncrease -= IncreaseTickMoney;
    }
    private void Start()
    {
        
        MoneyAmount = YandexGame.savesData.Money;
        _moneyPerTick = YandexGame.savesData.TickMoney;
        if (YandexGame.savesData.ClickMultiplier > 0)
            _moneyClickMultiplier = YandexGame.savesData.ClickMultiplier;
        else
            _moneyClickMultiplier = 1;
        UpdateTickMoneyText();
        
    }
    private void Update()
    {
        MoneyAmount += Time.deltaTime * _moneyPerTick;
        OnMoneyChange();
        SaveDataYandex();
    }
    private void UpdateTickMoneyText()
    {
        if (_moneyPerTick < 10000)
            _tickText.text = Math.Round(_moneyPerTick).ToString();
        else
            _tickText.text = ShortScaleString.parseFloat(_moneyPerTick, 1, 10000, true);
    }
    public void DoubleClick()
    {
        _clickMoneyIsDoubled = true;
        _moneyClickMultiplier *= 2;
    }
    public void HalfClick()
    {
        _clickMoneyIsDoubled = false;
        _moneyClickMultiplier /= 2;
    }
    public void DoubleTick()
    {
        _tickMoneyIsDoubled=true;
        _moneyPerTick *= 2;
        UpdateTickMoneyText();
    }
    public void HalfTick()
    {
        _tickMoneyIsDoubled = false;
        _moneyPerTick /= 2;
        UpdateTickMoneyText();
    }
    private void GoldenDicePop()
    {
        float MoneyGained;
        if (_moneyPerTick * 150 > _moneyClickMultiplier * 70)
            MoneyGained = _moneyPerTick * 150;
        else
            MoneyGained = _moneyClickMultiplier * 70;
        MoneyAmount += MoneyGained;
        GameObject Text = Instantiate(_clickText, _textSpawnPosition.position, Quaternion.identity, _clickCanvas);
        Text.GetComponent<Text>().color = Color.yellow;
        Text.GetComponent<Text>().fontSize = 50;
        Text.GetComponent<Text>().text = ShortScaleString.parseFloat(MoneyGained, 1, 10000, true).ToString();
    }
    private void SpendMoney(float Money)
    {
        MoneyAmount -= Money;
    }
    private void IncreaseTickMoney(float Increase)
    {
        if (_tickMoneyIsDoubled)
            Increase *= 2;
        _moneyPerTick+=Increase;
        UpdateTickMoneyText();

        Text Text = Instantiate(_clickText, _blueTextSpawnPosition.position, Quaternion.identity, _clickCanvas).GetComponent<Text>();

        Text.text = ShortScaleString.parseFloat(Increase, 1, 10000, true).ToString();
        Text.color = Color.blue;
    }
    private void IncreaseClickMoney(float Increase)
    {
        if (_clickMoneyIsDoubled)
            Increase *= 2;
        _moneyClickMultiplier += Increase;
        Text Text = Instantiate(_clickText, _blueTextSpawnPosition.position, Quaternion.identity, _clickCanvas).GetComponent<Text>();

        Text.text = ShortScaleString.parseFloat(Increase, 1, 10000, true).ToString();
        Text.color = Color.blue;
    }
    private void OnMoneyChange()
    {
        if(!_hasAchiemvent)
        {
            if(MoneyAmount> _theRichestManMoney)
            {
                _hasAchiemvent = true;
                onAchievmentGet?.Invoke(6);
            }
        }
        if(MoneyAmount < 10000)
            _moneyText.text = Math.Round(MoneyAmount).ToString();
        else
            _moneyText.text = ShortScaleString.parseFloat(MoneyAmount, 1, 10000, true);
    } 
    private void ClickMoneyGain(float Value)
    {
        MoneyAmount += (Value * _moneyClickMultiplier);
        GameObject Text = Instantiate(_clickText, _textSpawnPosition.position, Quaternion.identity, _clickCanvas);
        Text.GetComponent<Text>().text = ShortScaleString.parseFloat((Value * _moneyClickMultiplier), 1, 10000, true).ToString();
    }
    private void SaveDataYandex()
    {
        YandexGame.savesData.Money = MoneyAmount ;
        YandexGame.savesData.TickMoney = _moneyPerTick;
        YandexGame.savesData.ClickMultiplier = _moneyClickMultiplier;
    }
}
