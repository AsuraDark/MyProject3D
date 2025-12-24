using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _timeSpawnDelay;

    public event Action<Cube> ReleasedCube;

    private void OnEnable()
    {
        Spawn();
    }

    private void OnDisable()
    {
        StopSpawn();
    }

    protected override void Spawn()
    {
        _coroutine = StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        WaitForSeconds waitTime = new WaitForSeconds(_timeSpawnDelay);

        while (enabled)
        {
            GetCustomObject();
            yield return waitTime;
        }
    }

    private void StopSpawn()
    {
        StopCoroutine(_coroutine);
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.ResetStatus();

        cube.CubeDisapeared += OnCustomObjectDisapeared;
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);

        cube.CubeDisapeared -= OnCustomObjectDisapeared;

        ReleasedCube?.Invoke(cube);
    }
}