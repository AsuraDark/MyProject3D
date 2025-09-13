using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private ColorChanger _colorChanger;

    private int _minCountCubes = 2;
    private int _maxCountCubes = 6;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public List<Rigidbody> SpawnCubes(Cube startCube)
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
