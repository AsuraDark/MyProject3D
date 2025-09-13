using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spliter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Exploder _exploder;

    private float _minChanceSplit = 0f;
    private float _maxChanceSplit = 100f;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();
    }

    public bool IsSplitFailed(Cube cube)
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

        List<Rigidbody> spawnedRigidbodies = _spawner.SpawnCubes(cube);
        _exploder.Explosion(spawnedRigidbodies, cube);
        Destroy(cube.gameObject);
    }
}
