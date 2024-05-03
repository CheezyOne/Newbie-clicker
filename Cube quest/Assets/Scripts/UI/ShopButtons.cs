using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopButtons : MonoBehaviour
{
    [Header("Standart buttons vars")]
    [SerializeField] private int _buttonIndex;//For saving price data
    [SerializeField] private float _increaseFloat;
    [SerializeField] private float _price;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private Sprite _unblockedButtonSprite, _blockedButtonSprite;
    private Button _buttonComponent;
    private Image _imageComponent;

    [Header("Click and tick buttons")]
    private GameObject _imageAndDescription;
    [SerializeField] private Text _priceText;

    [Header("Dice upgrade")]
    public static Action onCubeUpgrade;
    [SerializeField] private GameObject[] _dices;
    [SerializeField] private GameObject _currentDice;
    [SerializeField] private Transform _dicePosition;
    [SerializeField] private bool _isCubeUpgrade;
    private int _cubeIndex = 0;

    private Color32 _darkerWhite = new(212, 212, 212, 255);
    private static int _upgradeCounter = 0;
    public static Action onXPUpgrade;
    public static Action<int> onAchievmentGet;
    public static Action<float> onDecreaseMoney;
    public static Action<float, string> onClickBuy, onTickBuy;
    private void Awake()
    {
        _imageComponent = GetComponent<Image>();
        _buttonComponent = GetComponent<Button>();
        if(!_isCubeUpgrade)
            _imageAndDescription = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        TryToUnlockButton();
        SaveData();
    }
    private void Start()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
        if(YandexGame.savesData.AllPrices[_buttonIndex]>0)
            _price = YandexGame.savesData.AllPrices[_buttonIndex];

        _priceText.text = ShortScaleString.parseFloat(_price, 0, 1000, true);
        if (_isCubeUpgrade)
        {
            _cubeIndex = YandexGame.savesData.CurrentDice;
            SetRollingDice();
        }
        
    }
    private void TryToUnlockButton()
    {
        if (IsEnoughMoney())
        {
            _imageComponent.color = _darkerWhite;
            _imageComponent.sprite = _unblockedButtonSprite;
            if(!_isCubeUpgrade)
                _imageAndDescription.SetActive(true);
            _buttonComponent.interactable = true;
        }
        else
        {
            _imageComponent.sprite = _blockedButtonSprite;
            _buttonComponent.interactable = false;
        }
    }
    private void SaveData()
    {
        YandexGame.savesData.AllPrices[_buttonIndex] = _price;
    }
    private bool IsEnoughMoney()
    {
        if (_moneyManager.MoneyAmount >= _price)
            return true;
        return false;

    }
    public void TickButton()
    {
        if (IsEnoughMoney())
        {
            onDecreaseMoney?.Invoke(_price);
            onTickBuy?.Invoke(_increaseFloat, "Tick");
            _price *= 1.1f;
            _priceText.text = ShortScaleString.parseFloat(_price, 0, 1000, true);
        }
    }
    public void ClickButton()
    {
        if (IsEnoughMoney())
        {
            onDecreaseMoney?.Invoke(_price);
            onClickBuy?.Invoke(_increaseFloat, "Click");
            _price *= 1.1f;
            _priceText.text = ShortScaleString.parseFloat(_price, 0, 1000, true);
        }
    }
    public void UpgradeCubeFunc()
    {
        if (IsEnoughMoney())
        { 
            _cubeIndex++;
            YandexGame.savesData.CurrentDice = _cubeIndex;
            Debug.Log(YandexGame.savesData.CurrentDice);
            onCubeUpgrade?.Invoke();
            onDecreaseMoney?.Invoke(_price);
            _price *= 130f;
            _priceText.text = ShortScaleString.parseFloat(_price, 1, 10000, true).ToString();
            SetRollingDice();
        }
    }
    private void SetRollingDice()
    {
        _currentDice.SetActive(false);
        _currentDice = _dices[_cubeIndex];
        _currentDice.SetActive(true);
        if (_cubeIndex == _dices.Length - 1)
            gameObject.SetActive(false);
    }

    public void IncreaseExperience()
    {
        if (IsEnoughMoney())
        {
            onDecreaseMoney?.Invoke(_price);
            onXPUpgrade?.Invoke();
            _price *= 69f;
            _priceText.text = ShortScaleString.parseFloat(_price, 1, 10000, true).ToString();
        }
    }
    public void InreaseUpgradeCounter()
    {
        _upgradeCounter++;
        if(_upgradeCounter>=200)
            onAchievmentGet?.Invoke(7);
    }
}
