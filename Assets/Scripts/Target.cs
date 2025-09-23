using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private Vector3[] _path;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;
    private int _currentIndexPath = 0;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ResetDirection();
    }

    private void FixedUpdate()
    {
        if (_path[_currentIndexPath].x <= 0)
        {
            if(_path[_currentIndexPath].z <= 0)
            {
                if (_path[_currentIndexPath].x - _rigidbody.position.x >= -0.1 &&
                    _path[_currentIndexPath].z - _rigidbody.position.z >= -0.1)
                {
                    ResetIndex();
                }
            }
            else if (_path[_currentIndexPath].z >= 0)
            {
                if (_path[_currentIndexPath].x - _rigidbody.position.x >= -0.1 &&
                    _path[_currentIndexPath].z - _rigidbody.position.z <= 0.1)
                {
                    ResetIndex();
                }
            }
        }
        else if (_path[_currentIndexPath].x >= 0)
        {
            if (_path[_currentIndexPath].z <= 0)
            {
                if (_path[_currentIndexPath].x - _rigidbody.position.x <= 0.1 &&
                    _path[_currentIndexPath].z - _rigidbody.position.z >= -0.1)
                {
                    ResetIndex();
                }
            }
            else if (_path[_currentIndexPath].z >= 0)
            {
                if (_path[_currentIndexPath].x - _rigidbody.position.x <= 0.1 &&
                    _path[_currentIndexPath].z - _rigidbody.position.z <= 0.1)
                {
                    ResetIndex();
                }
            }
        }

        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime * _speed);
    }

    private void ResetDirection()
    {
        _direction = (_path[_currentIndexPath] - _rigidbody.position).normalized;
    }

    private void ResetIndex()
    {
        _currentIndexPath++;

        if (_currentIndexPath >= _path.Length)
        {
            _currentIndexPath = 0;
        }

        ResetDirection();
    }
}
