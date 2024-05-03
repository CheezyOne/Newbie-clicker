using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenDiceGetter : MonoBehaviour
{
    public static Action onGoldenDicePop;
    public static Action<int> onAchievmentGet;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SendRay();
    }
    private void SendRay()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        if (hit.transform == null)
            return;
        if(hit.transform.TryGetComponent<Dice>(out Dice DiceComponent))
        {
            if (DiceComponent.IsGolden)
            {
                DiceComponent.DestroyCube();
                onGoldenDicePop?.Invoke();
                onAchievmentGet?.Invoke(8);
            }
        }

    }
}
