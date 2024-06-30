using System;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    [SerializeField] private GameObject _resetConfirmation;
    [SerializeField] private ShopButtons[] _buttons;
    [SerializeField] private SteveUpgrade _upgrade;
    [SerializeField] private BoostersButtons _boosters;

    public static Action Reset;
    public void ResetAction()
    {
        Reset?.Invoke();
        _resetConfirmation.SetActive(false);
        foreach (ShopButtons Button in _buttons)
        {
            Button.ResetButtons();
        }
        _upgrade.ResetLevel();
        _boosters.ResetBoosters();
    }
    public void CancelReset()
    {
        _resetConfirmation.SetActive(false);
    }
    public void StartConfirmation()
    {
        _resetConfirmation.SetActive(true);
    }
}
