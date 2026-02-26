using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDetector : MonoBehaviour
{
    public event Action<Base> BaseDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Base @base))
        {
            BaseDetected?.Invoke(@base);
        }
    }
}
