using System;
using UnityEditor;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    [SerializeField] private FlagPreviewer _flagPreview;
    [SerializeField] private Flag _flag;
    [SerializeField] private Transform _camera;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private bool _canCreatePreviewFlag;
    [SerializeField] private bool _canCreateFlag;

    private RaycastHit _hitInfo;

    public event Action<Flag> FlagCreated;

    private void Awake()
    {
        _camera = FindAnyObjectByType<Camera>().transform;
        _player = FindAnyObjectByType<Player>();
    }

    private void OnEnable()
    {
        _player.BaseBuilding += CreateFlag;
    }

    private void OnDisable()
    {
        _player.BaseBuilding -= CreateFlag;
    }

    private void FixedUpdate()
    {
        CreatePreviewFlag(_canCreatePreviewFlag);
    }

    private void OnMouseDown()
    {
        _canCreatePreviewFlag = !_canCreatePreviewFlag;
    }

    private void OnMouseUp()
    {
        _canCreateFlag = !_canCreateFlag;
    }

    private void CreatePreviewFlag(bool canDrawPrevieFlag)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 direction = (mousePos - _camera.position).normalized;

        if (Physics.Raycast(_camera.position, direction, out _hitInfo, float.MaxValue, _layerMask) && canDrawPrevieFlag)
        {
            if (!_flagPreview.IsActive)
            {
                _flagPreview.Enable();
            }

            _flagPreview.SetPosition(_hitInfo.point);
        }
        else
        {
            _flagPreview.Disable();
        }
    }

    public void CreateFlag()
    {
        if (_hitInfo.point != null && _canCreateFlag && _canCreatePreviewFlag)
        {
            if (!_flag.IsActive)
            {
                _flag.Enable();
            }

            _flag.SetPosition(_hitInfo.point);

            _canCreatePreviewFlag = false;

            _canCreateFlag = false;

            FlagCreated?.Invoke(_flag);
        }
    }
}