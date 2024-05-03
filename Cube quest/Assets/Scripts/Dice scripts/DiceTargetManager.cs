using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTargetManager : MonoBehaviour
{
    public static Action<float> onMoneyIncrease, onClickIncrease, onTickIncrease;
    private void OnEnable()
    {
        Dice.onDiceStop += DecideTarget;
    }
    private void OnDisable()
    {
        Dice.onDiceStop -= DecideTarget;
    }
    private void DecideTarget(float Value, float ValueToIncrease ,string Target)
    {
        switch (Target)
        {
            case "Money":
                {
                    onMoneyIncrease?.Invoke(Value);
                    break;
                }
            case "Tick":
                {
                    onTickIncrease?.Invoke(Value * ValueToIncrease);
                    break;
                }
            case "Click":
                {
                    onClickIncrease?.Invoke(Value* ValueToIncrease);
                    break; 
                }
        }
    }
}
