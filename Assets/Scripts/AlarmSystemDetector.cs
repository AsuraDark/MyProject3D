using System;
using UnityEngine;

public class AlarmSystemDetector : MonoBehaviour
{
    private bool _isAlarmEnable = false;

    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Thief>(out Thief thief))
        {
            if (_isAlarmEnable == false)
            {
                _isAlarmEnable = true;
                ThiefEntered?.Invoke();
            }

            else
            {
                _isAlarmEnable = false;
                ThiefExited?.Invoke();
            }
        }
    }
}
