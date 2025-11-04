using System;
using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    private bool _isThiefEntered = false;

    public event Action ThiefEntered;
    public event Action ThiefExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Thief>(out Thief thief))
        {
            if(_isThiefEntered == false)
            {
                _isThiefEntered = true;
                ThiefEntered?.Invoke();
            }
            else
            {
                _isThiefEntered = false;
                ThiefExited?.Invoke();
            }
        }
    }
}
