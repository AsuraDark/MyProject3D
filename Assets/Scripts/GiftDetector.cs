using System;
using UnityEngine;

public class GiftDetector : MonoBehaviour
{
    public event Action<Gift> GiftDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gift gift))
        {
            GiftDetected?.Invoke(gift);
        }
    }
}
