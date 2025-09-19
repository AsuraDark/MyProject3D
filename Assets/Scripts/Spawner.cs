using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _timeSpawnDelay;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private Vector3 _direction;

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
        cube.Init(transform.position, _direction);

        cube.CubeDisapeared += _pool.Release;
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);

        cube.CubeDisapeared -= _pool.Release;
    }
}
