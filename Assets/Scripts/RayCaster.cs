using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class RayCaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private Ray _ray;
    private RaycastHit _hit;

    public event Action<Cube> RaycastHitted;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_ray, out _hit);
    }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
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
        if (_hit.collider.TryGetComponent<Cube>(out Cube cube))
        {
            RaycastHitted?.Invoke(cube);
        }
    }
}
