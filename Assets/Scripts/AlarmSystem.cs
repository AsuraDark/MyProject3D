using System;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    private bool _isAlarmEnable = false;

    public event Action AlarmEnabled;
    public event Action AlarmDisabled;

    public void StartAlarm()
    {
        _isAlarmEnable = true;
        AlarmEnabled?.Invoke();
    }

    public void StopAlarm()
    {
        _isAlarmEnable = false;
        AlarmDisabled?.Invoke();
    }
}
