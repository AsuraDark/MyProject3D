using System;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _spawnLength;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private float speed;

    private Rigidbody _rigidbody;
    private Vector3 _direction;

    public Action<Cube> CubeDisapeared;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);
    }

    public void Init(Vector3 spawnCenter, Vector3 direction)
    {
        transform.position = CreateRandomPosition(spawnCenter);
        transform.rotation = Quaternion.identity;
        _direction = direction.normalized;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private Vector3 CreateRandomPosition(Vector3 spawnCenter)
    {
        return new Vector3(Random.Range(spawnCenter.x - _spawnLength, spawnCenter.x + _spawnLength), 
                           _spawnHeight, 
                           Random.Range(spawnCenter.z - _spawnLength, spawnCenter.z + _spawnLength));
    }
}
