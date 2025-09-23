using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Target _target;
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    public event Action<Cube> Disappeared;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ResetDirection();
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime * _speed);
    }

    public void Init(Vector3 direction, Target target)
    {
        _target = target;
        _rigidbody.rotation = Quaternion.identity;
        _direction = direction.normalized;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void ResetDirection()
    {
        _direction = (_target.transform.position - _rigidbody.position).normalized;
    }

    public void Disappear()
    {
        Disappeared.Invoke(this);
    }
}
