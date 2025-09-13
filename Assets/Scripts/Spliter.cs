using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
[RequireComponent(typeof(Exploder))]
public class Spliter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private RayCaster _rayCaster;

    private float _minChanceSplit = 0f;
    private float _maxChanceSplit = 100f;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();
        _rayCaster = Camera.main.GetComponent<RayCaster>();
    }

    private void OnEnable()
    {
        _rayCaster.RaycastHitted += SplitCube;
    }

    private void OnDisable()
    {
        _rayCaster.RaycastHitted -= SplitCube;
    }

    private bool IsSplitFailed(Cube cube)
    {
        float chance = Random.Range(_minChanceSplit, _maxChanceSplit);
        return chance > cube.CurrentChanceSplit;
    }

    public void SplitCube(Cube cube)
    {
        if (IsSplitFailed(cube))
        {
            _exploder.Explosion(cube);
            Destroy(cube.gameObject);
            return;
        }

        List<Cube> spawnedCubes = _spawner.SpawnCubes(cube);
        _exploder.Explosion(spawnedCubes, cube);
        Destroy(cube.gameObject);
    }
}
