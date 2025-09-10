using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private InputReader _inputReader;

    private Ray _ray;
    private RaycastHit _hit;

    private bool _hasValidTarget = false;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _hasValidTarget = false;

        if (Physics.Raycast(_ray, out _hit))
            if (_hit.collider.TryGetComponent<Cube>(out _))
                _hasValidTarget = true;
    }

    private void OnEnable()
    {
        _inputReader.MouseClicked += SelectObject;
    }

    private void OnDisable()
    {
        _inputReader.MouseClicked -= SelectObject;
    }

    private void SelectObject()
    {
        if (_hasValidTarget == false)
            return;

        if (_hit.collider.TryGetComponent<Cube>(out Cube cube))
            _spawner.Split(cube);
    }
}
