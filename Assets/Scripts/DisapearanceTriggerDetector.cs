using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearanceTriggerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Gift>(out Gift gift))
        {
            gift.Disappear();
        }
    }
}
