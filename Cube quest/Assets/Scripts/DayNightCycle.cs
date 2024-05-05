using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public static Action<int> onAchievmentGet;
    public void DayEnd()
    {
        onAchievmentGet?.Invoke(1);
    }
}
