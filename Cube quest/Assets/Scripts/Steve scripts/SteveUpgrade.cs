using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SteveUpgrade : MonoBehaviour
{
    public static Action<int> onSteveUpgrade;
    public static Action<int> onAchievmentGet;
    [SerializeField] private Image _xpBar;
    [SerializeField] private float[] _xpForEachLevel;
    private float _currentXp, _xpMultiplier=1;
    private int _level;
    private void OnEnable()
    {
        SteveHolder.onSteveClick += GetXP;
        BoostersButtons.onXPBoostActive += DoubleXP;
        BoostersButtons.onXPBoostDeactive += HalfXP;
        ShopButtons.onXPUpgrade += IncreaseXP;
    }
    private void OnDisable()
    {
        SteveHolder.onSteveClick -= GetXP;
        BoostersButtons.onXPBoostActive -= DoubleXP;
        BoostersButtons.onXPBoostDeactive -= HalfXP;
        ShopButtons.onXPUpgrade -= IncreaseXP;
    }
    private void Start()
    {
        if (YandexGame.savesData.XPMultiplyer>0)
            _xpMultiplier = YandexGame.savesData.XPMultiplyer;
        else
            _xpMultiplier = 1;
        _currentXp = YandexGame.savesData.CurrentXP;
        _level = YandexGame.savesData.CurrentLevel;
    }
    private void HalfXP()
    {
        _xpMultiplier/=2;
    }
    private void DoubleXP()
    {
        _xpMultiplier*=2;
    }
    private void IncreaseXP()
    {
        _xpMultiplier++;
        YandexGame.savesData.XPMultiplyer++;
    }
    private void GetXP()
    {
        if (_level == _xpForEachLevel.Length-1)
            return;
        _currentXp += _xpMultiplier;
        YandexGame.savesData.CurrentXP = _currentXp;
        _xpBar.fillAmount = _currentXp/ _xpForEachLevel[_level];
        if (_currentXp >= _xpForEachLevel[_level])
        {
            _xpBar.fillAmount = 0;
            _level++;
            onSteveUpgrade?.Invoke(_level);
            YandexGame.savesData.CurrentLevel = _level;
            if (_level == _xpForEachLevel.Length-1)
            {
                onAchievmentGet?.Invoke(4);
                _xpBar.fillAmount = 100f;
            }
            _currentXp = 0;
        }
    }
}
