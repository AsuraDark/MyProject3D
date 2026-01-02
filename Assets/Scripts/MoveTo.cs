using UnityEngine;
using DG.Tweening;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Vector3 _newPosition;
    [SerializeField] private float _duration;
    
    private void Start()
    {
        transform.DOMove(_newPosition, _duration);
    }
}
