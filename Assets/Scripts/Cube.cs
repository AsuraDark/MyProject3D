using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(PlatformCollisionDetector))]
[RequireComponent(typeof(DisappearanceTimer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private PlatformCollisionDetector _collisionDetector;
    [SerializeField] private DisappearanceTimer _disappearanceTimer;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _minSpawnLength = -5f;
    [SerializeField] private float _maxSpawnLength = 5f;
    [SerializeField] private float _spawnHeight = 20f;
    [SerializeField] private float _minHeightDisappearance = 0f;

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

    public void Reset()
    {
        _collisionDetector.Reset();
        _colorChanger.Reset();

        transform.position = CreateRandomPosition();
        _rigidbody.velocity = Vector3.zero;
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

    private void Update()
    {
        if (transform.position.y < _minHeightDisappearance)
        {
            CubeDisapeared?.Invoke(this);
        }
    }
}