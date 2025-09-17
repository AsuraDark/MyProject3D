using System;
using UnityEngine;

public class PlatformCollisionDetector : MonoBehaviour
{
    private bool _isFirstCollision = false;

    public event Action PlatformCollisionDetected;

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isFirstCollision && collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isFirstCollision = true;

            PlatformCollisionDetected?.Invoke();
        }
    }

    public void ResetStatus()
    {
        _isFirstCollision = false;
    }
}