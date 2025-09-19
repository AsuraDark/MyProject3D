using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _timeSpawnDelay;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _spawnLength;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private Coroutine _coroutine;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize,
            collectionCheck: true
            );
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(StartSpawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private Vector3 CreateRandomPosition(Vector3 spawnCenter)
    {
        return new Vector3(Random.Range(spawnCenter.x - _spawnLength, spawnCenter.x + _spawnLength),
                           _spawnHeight,
                           Random.Range(spawnCenter.z - _spawnLength, spawnCenter.z + _spawnLength));
    }

    private IEnumerator StartSpawn()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_timeSpawnDelay);

        while (enabled)
        {
            _pool.Get();

            yield return waitTime;
        }
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = CreateRandomPosition(transform.position);
        cube.Init(_direction);

        cube.CubeDisapeared += _pool.Release;
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        cube.CubeDisapeared -= _pool.Release;
    }
}
