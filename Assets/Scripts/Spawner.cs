using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private Exploder _exploder;

    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;

    public void Split(Cube cube)
    {
        if (IsSplitFailed(cube))
        {
            Destroy(cube.gameObject);
            return;
        }

        List<Rigidbody> spawnedRigidbodies = SpawnCubes(cube);
        _exploder.Explosion(spawnedRigidbodies, cube.transform.position);
        Destroy(cube.gameObject);
    }

    private bool IsSplitFailed(Cube cube)
    {
        float chance = Random.Range(0f, 100f);

        return chance > cube.CurrentChanceSplit;
    }

    private List<Rigidbody> SpawnCubes(Cube startCube)
    {
        List<Rigidbody> rigidbodies = new();
        Vector3 spawnPos = startCube.transform.position;
        int count = Random.Range(_minCountCubes, _maxCountCubes + 1);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_prefab, spawnPos, Quaternion.identity).GetComponent<Cube>();
            newCube.Init(startCube.CurrentChanceSplit, startCube.transform.localScale, _colorChanger.RandomColor);
            rigidbodies.Add(newCube.GetComponent<Rigidbody>());
        }

        return rigidbodies;
    }
}
