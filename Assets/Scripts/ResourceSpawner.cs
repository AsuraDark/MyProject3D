using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _spawnHeight;

    private ObjectPool<Resource> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<Resource>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (resource) => ActionOnGet(resource),
        actionOnRelease: (resource) => ActionOnRelease(resource),
        actionOnDestroy: (resource) => Destroy(resource.gameObject),
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize,
        collectionCheck: true
        );
    }

    private void Start()
    {
        StartCoroutine(StartSpawnResources(_spawnDelay));
    }

    public void ActionOnGet(Resource resource)
    {
        resource.gameObject.SetActive(true);

        resource.transform.position = new Vector3(
            Random.Range(-_spawnRange, _spawnRange),
            _spawnHeight,
            Random.Range(-_spawnRange, _spawnRange));

        resource.ResourceTransfered += ReleaseResource;
    }

    public void ActionOnRelease(Resource resource)
    {
        resource.gameObject.SetActive(false);
    }


    private IEnumerator StartSpawnResources(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            yield return wait;

            SpawnResources();
        }
    }

    private Resource SpawnResources()
    {
        _pool.Get(out Resource gameObject);

        return gameObject;
    }

    private void ReleaseResource(Resource resource)
    {
        resource.ResourceTransfered -= ReleaseResource;

        _pool.Release(resource);
    }
}