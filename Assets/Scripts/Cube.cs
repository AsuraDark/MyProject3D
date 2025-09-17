using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(PlatformCollisionDetector))]
[RequireComponent(typeof(DisappearanceTimer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minSpawnLength = -5f;
    [SerializeField] private float _maxSpawnLength = 5f;
    [SerializeField] private float _spawnHeight = 20f;

    private ColorChanger _colorChanger;
    private PlatformCollisionDetector _collisionDetector;
    private DisappearanceTimer _disappearanceTimer;
    private Rigidbody _rigidbody;

    public Action<Cube> CubeDisapeared;

    private void Awake()
    {
        _collisionDetector = GetComponent<PlatformCollisionDetector>();
        _colorChanger = GetComponent<ColorChanger>();
        _disappearanceTimer = GetComponent<DisappearanceTimer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _collisionDetector.PlatformCollisionDetected += OnCollisionDetected;
        _disappearanceTimer.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        _collisionDetector.PlatformCollisionDetected -= OnCollisionDetected;
        _disappearanceTimer.TimerEnded -= OnTimerEnded;
    }

    public void ResetStatus()
    {
        _collisionDetector.ResetStatus();
        _colorChanger.ResetStatus();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.position = CreateRandomPosition();
    }

    private Vector3 CreateRandomPosition()
    {
        return new Vector3(Random.Range(_minSpawnLength, _maxSpawnLength), _spawnHeight, Random.Range(_minSpawnLength, _maxSpawnLength));
    }

    private void OnCollisionDetected()
    {
        _colorChanger.SetRandomColor();
        _disappearanceTimer.StartTimer();
    }

    private void OnTimerEnded()
    {
        CubeDisapeared?.Invoke(this);
    }
}