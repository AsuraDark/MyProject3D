using System;
using UnityEngine;

public class PlatformCollisionDetector : MonoBehaviour
{
    private string _nameTagPlatform = "Platform";
    private bool _isFirstCollision = false;

    public event Action PlatformCollisionDetected;

    public void Reset()
    {
        _isFirstCollision = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!_isFirstCollision && collision.collider.CompareTag(_nameTagPlatform))
        {
            _isFirstCollision = true;

            PlatformCollisionDetected?.Invoke();
        }
    }
}