using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    public event Action<Cube> CubeDisapeared;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);
    }

    public void Init(Vector3 direction)
    {
        transform.rotation = Quaternion.identity;
        _direction = direction.normalized;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void OnCubeDisapeared()
    {
        CubeDisapeared.Invoke(this);
    }
}
