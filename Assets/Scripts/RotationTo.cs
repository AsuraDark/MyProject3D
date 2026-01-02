using DG.Tweening;
using UnityEngine;

public class RotationTo : MonoBehaviour
{
    [SerializeField] private Vector3 _rotate;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DORotate(_rotate, _duration);
    }
}