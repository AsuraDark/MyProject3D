using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class GiftSpawner : MonoBehaviour
{
    [SerializeField] private Gift _prefab;
    [SerializeField] private float _timeSpawnDelay;
    [SerializeField] private float _spawnLength;
    [SerializeField] private float _spawnHeight;

    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private Coroutine _coroutine;

    private ObjectPool<Gift> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Gift>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (gift) => ActionOnGet(gift),
            actionOnRelease: (gift) => ActionOnRelease(gift),
            actionOnDestroy: (gift) => Destroy(gift.gameObject),
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

    private void ActionOnGet(Gift gift)
    {
        gift.gameObject.SetActive(true);
        gift.transform.position = CreateRandomPosition(transform.position);

        gift.Disappeared += _pool.Release;
    }

    private void ActionOnRelease(Gift gift)
    {
        gift.gameObject.transform.parent = null;
        gift.gameObject.SetActive(false);

        gift.Disappeared -= _pool.Release;
    }
}
