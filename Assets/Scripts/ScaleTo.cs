using DG.Tweening;
using UnityEngine;

public class ScaleTo : MonoBehaviour
{
    [SerializeField] private float _scale;
    [SerializeField] private float _duration;

    private void Start()
    {
        transform.DOScale(_scale, _duration);
    }
}
