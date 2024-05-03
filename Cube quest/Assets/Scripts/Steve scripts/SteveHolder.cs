using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SteveHolder : MonoBehaviour
{
    public static Action onSteveClick, onHypeClick;
    public static Action<int> onAchievmentGet;
    [SerializeField] private MeshRenderer[] _steveParts;
    [SerializeField] private Texture[] _russianSkins, _englishSkins;
    [SerializeField] private string[] _russianNames, _englishNames;
    [SerializeField] private Text _currentName;
    [SerializeField] private GameObject[] _steves;
    private bool _isEnglish = true;
    private void Start()
    {
        if (YandexGame.lang == "ru" || Application.systemLanguage == SystemLanguage.Russian)
            _isEnglish = false;
        ChangeSteve(YandexGame.savesData.CurrentLevel);
    }
    private void OnEnable()
    {
        SteveClickRegister.onSteveClick += WasClicked;
        SteveUpgrade.onSteveUpgrade += ChangeSteve;
    }
    private void OnDisable()
    {
        SteveClickRegister.onSteveClick -= WasClicked;
        SteveUpgrade.onSteveUpgrade -= ChangeSteve;
    }
    private void GetHypingSteveRandomly()
    {
        _steves[1].SetActive(false);
        _steves[0].SetActive(true);
        if (UnityEngine.Random.Range(0,10000) <= 69)
        {
            onAchievmentGet?.Invoke(2);
            onHypeClick?.Invoke();
            _steves[1].SetActive(true);
            _steves[0].SetActive(false);
        }
    }
    private void WasClicked()
    {
        GetHypingSteveRandomly();
        onSteveClick?.Invoke();
    }
    private void ChangeSteve(int Index)
    {
        if (!_isEnglish)
        {
            foreach (MeshRenderer StevePart in _steveParts)
            {
                StevePart.material.mainTexture = _russianSkins[Index];
            }
            _currentName.text = _russianNames[Index];
        }
        else
        {
            foreach (MeshRenderer StevePart in _steveParts)
            {
                StevePart.material.mainTexture = _englishSkins[Index];
            }
            _currentName.text = _englishNames[Index];
        }
    }
}