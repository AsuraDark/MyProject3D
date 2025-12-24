using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected float _repeatRate;
    [SerializeField] protected int _poolCapacity;
    [SerializeField] protected int _poolMaxSize;

    protected Coroutine _coroutine;

    protected ObjectPool<T> _pool;

    public event Action SpawnedObject;

    public int SpawnedObjects { get; protected set; } = 0;

    public int CreatedObjects => _pool.CountAll;

    public int ActiveObjects => _pool.CountActive;


    protected void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (customObject) => ActionOnGet(customObject),
            actionOnRelease: (customObject) => ActionOnRelease(customObject),
            actionOnDestroy: (customObject) => Destroy(customObject),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize,
            collectionCheck: true
        );
    }

    protected virtual void Spawn()
    {
        GetCustomObject();
    }

    protected T GetCustomObject()
    {
        return _pool.Get();
    }

    protected virtual void ActionOnGet(T customObject)
    {
        customObject.gameObject.SetActive(true);
        SpawnedObject?.Invoke();
        SpawnedObjects++;
    }

    protected virtual void ActionOnRelease(T customObject)
    {
        customObject.gameObject.SetActive(false);
    }

    protected virtual void OnCustomObjectDisapeared(T customObject)
    {
        _pool.Release(customObject);
    }
}