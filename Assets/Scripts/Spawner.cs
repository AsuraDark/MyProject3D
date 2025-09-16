using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize,
            collectionCheck: true
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private Cube GetCube()
    {
        return _pool.Get();
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Reset();
        cube.gameObject.SetActive(true);

        cube.CubeDisapeared += OnCubeDisapeared;
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        cube.CubeDisapeared -= OnCubeDisapeared;
    }

    private void OnCubeDisapeared(Cube cube)
    {
        _pool.Release(cube);
    }
}