using DG.Tweening;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRendeer;
    [SerializeField] private Color _color;
    [SerializeField] private float _duration;

    private void Start()
    {
        _meshRendeer.material.DOColor(_color, _duration);
    }
}
