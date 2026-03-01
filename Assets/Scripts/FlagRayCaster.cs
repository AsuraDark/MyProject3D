using System;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class FlagRayCaster : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _layerMask;
    private FlagManager _flagManager;

    public event Action RayCastTargeted;
    public event Action RayCastNotTargeted;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _camera = Camera.main.transform;
    }

    private void OnEnable()
    {
        _inputReader._leftMouseButtonisClicked += StartRayCast;
    }

    private void OnDisable()
    {
        _inputReader._leftMouseButtonisClicked -= StartRayCast;
    }

    private void StartRayCast()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 direction = (mousePos - _camera.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(_camera.position, direction, out hit, float.MaxValue))
        {
            if (hit.collider.TryGetComponent(out FlagManager flagManager))
            {
                _flagManager = flagManager;
                _flagManager.ChangePosibilityCreateFlag();
            }
            else if(hit.collider.TryGetComponent(out GameZone gameZone))
            {
                _flagManager.CreateFlag();
            }
        }
    }
}