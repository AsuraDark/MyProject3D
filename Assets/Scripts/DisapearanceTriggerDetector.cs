using UnityEngine;

public class DisapearanceTriggerDetector : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            cube.CubeDisapeared?.Invoke(cube);
        }
    }
}