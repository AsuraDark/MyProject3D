using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearanceTriggerDetector : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            cube.OnCubeDisapeared();
        }
    }
}
