using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitChecker : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RayCaster _rayCaster;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _rayCaster = GetComponent<RayCaster>();
    }

    private void OnEnable()
    {
        _inputReader.MouseClicked += CheckSpawnCube;
    }

    private void OnDisable()
    {
        _inputReader.MouseClicked -= CheckSpawnCube;
    }

    private void CheckSpawnCube()
    {
        if (_rayCaster.Hit.collider.TryGetComponent<Cube>(out Cube cube))
        {
            cube.Split();
        }
    }
}
