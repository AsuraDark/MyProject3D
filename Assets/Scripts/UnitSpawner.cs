using UnityEngine;
using UnityEngine.Pool;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Unit> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<Unit>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (unit) => ActionOnGet(unit),
        actionOnRelease: (unit) => ActionOnRelease(unit),
        actionOnDestroy: (unit) => Destroy(unit.gameObject),
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize,
        collectionCheck: true
        );
    }

    public Unit CreateUnit()
    {
        _pool.Get(out Unit unit);

        return unit;
    }

    public void ActionOnGet(Unit unit)
    {
        Vector3 unitPosition = transform.position;

        unit.gameObject.SetActive(true);
        unit.transform.SetParent(transform);
        unit.transform.position = unitPosition;
        unit.Init();
    }

    public void ActionOnRelease(Unit unit)
    {
        unit.gameObject.SetActive(false);
    }

    public void ReleaseGameObject(Unit unit)
    {
        _pool.Release(unit);
    }
}